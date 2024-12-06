using BookOrder.Business.Dto;
using BookOrder.Business.Repo.Interfaces;
using BookOrder.Data.Domain;
using BookOrder.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo
{
    public class BookRepository : IBookRepository
    {
        private readonly BookOrderDbContext _dbContext;

        public BookRepository(BookOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultDto<bool>> CreateAsync(BookCreateDto dto, CancellationToken cancellationToken)
        {
            Book book = new Book
            { 
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Author = dto.Author,
                Stock = dto.Stock,
                Amount = dto.Amount,
            };

            _dbContext.Books.Add(book);

            if (await _dbContext.SaveChangesAsync(cancellationToken) > 0)
                return new ResultDto<bool>(true);
            else
                return new ResultDto<bool>(false, ResultMessages.BOOK_CANNOT_CREATE);
        }

        public async Task<ResultDto<bool>> UpdateAsync(BookUpdateDto dto, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Books.Where(w => w.Id == dto.Id).FirstOrDefaultAsync();

            if (book == null)
                return new ResultDto<bool>(false, ResultMessages.BOOK_CANNOT_FIND);

            book.Name = dto.Name;
            book.Author = dto.Author;
            book.Stock = dto.Stock;
            book.Amount = dto.Amount;

            _dbContext.Books.Update(book);

            if (await _dbContext.SaveChangesAsync(cancellationToken) > 0)
                return new ResultDto<bool>(true);
            else
                return new ResultDto<bool>(false, ResultMessages.BOOK_CANNOT_UPDATE);
        }

        public async Task<ResultDto<List<BookDto>>> GetAllAsync(PaginationDto pagination, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Books
                             .Skip((pagination.Page - 1) * pagination.Size)
                             .Take(pagination.Size)
                             .Select(s => new BookDto
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 Amount = s.Amount,
                                 Stock = s.Stock,
                                 Author = s.Author,                                 
                             })
                             .ToListAsync(cancellationToken);

            return new ResultDto<List<BookDto>>(data);
        }
    }
}
