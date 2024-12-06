using BookOrder.Business.Dto;
using BookOrder.Business.Repo.Interfaces;
using BookOrder.Data.Domain;
using BookOrder.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo
{
    public class OrderRepository : IOrderRepository
    {
        BookOrderDbContext _dbContext;
        readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public OrderRepository(BookOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultDto<List<OrderDetailDto>>> FindOrdersAsync(OrderSearchDto searchDto, CancellationToken cancellationToken)
        {
            var query = _dbContext.Orders.AsQueryable();

            if (searchDto.StartDate.HasValue && searchDto.StartDate > DateTime.MinValue)
            {
                query = query.Where(w => w.OrderDate >= searchDto.StartDate.Value);
            }

            if (searchDto.EndDate.HasValue && searchDto.EndDate > DateTime.MaxValue)
            {
                query = query.Where(w => w.OrderDate <= searchDto.EndDate);
            }

            if (searchDto.IsDesc)
            {
                query = query.OrderByDescending(w => w.OrderDate);
            }
            else
            {
                query = query.OrderBy(w => w.OrderDate);
            }

            var data = await query.Select(s => new OrderDetailDto
            {
                Id = s.Id,
                OrderDate = s.OrderDate,
                Quantity = s.Quantity,
                BookDetail = new BookDto
                {
                    Id = s.Book.Id,
                    Name = s.Book.Name,
                    Author = s.Book.Author,
                    Amount = s.Book.Amount,
                },
                CustomerDetail = new CustomerDto
                {
                    Id = s.Customer.Id,
                    Name = s.Customer.Name,
                    LastName = s.Customer.LastName,
                },
            })
                        .ToListAsync(cancellationToken);

            return new ResultDto<List<OrderDetailDto>>(data);
        }

        public async Task<ResultDto<bool>> OrderBookAsync(OrderCreateDto dto, CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                /*
                 * Developer Yorumu:
                    Buradaki trasnaction'lar InMemory db'nin desteklememesi sebebiyle kapatılmıştır,
                    Gerçek dünyada DB'de güvenli stok düşülmesi adına buraların açılması gerekir.
                 */

                //using (var transaction = _dbContext.Database.BeginTransaction())
                //{
                try
                {
                    var book = await _dbContext.Books
                                   .Where(w => w.Id == dto.BookId)
                                   .FirstOrDefaultAsync(cancellationToken);

                    if (book == null || book?.Id == Guid.Empty)
                        return new ResultDto<bool>(ResultMessages.ORDER_CANNOT_FIND_BOOK);

                    if (book?.Stock < dto.Quantity) 
                        return new ResultDto<bool>(ResultMessages.ORDER_NOT_ENOUGHT_STOCK);

                    DecreaseStock(book, dto.Quantity);

                    Order entity = new Order
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = dto.CustomerId,
                        BookId = dto.BookId,
                        Quantity = dto.Quantity,
                        OrderDate = dto.OrderDate,
                        TotalAmount = dto.Quantity * book!.Amount,
                    };

                    _dbContext.Orders.Add(entity);

                    if (await _dbContext.SaveChangesAsync(cancellationToken) > 0)
                    {
                        //transaction.Commit();
                        return new ResultDto<bool>(true);
                    }
                    else
                    {
                        //transaction.Rollback();
                        return new ResultDto<bool>(false, ResultMessages.ORDER_CANNOT_CREATE);
                    }
                }
                catch (Exception e)
                {
                    //transaction.Rollback();
                    return new ResultDto<bool>(ResultMessages.ORDER_CANNOT_CREATE);
                }
                //}
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void DecreaseStock(Book book, int quantity)
        {
            book!.Stock -= quantity;
            _dbContext.Books.Update(book);
        }
    }
}
