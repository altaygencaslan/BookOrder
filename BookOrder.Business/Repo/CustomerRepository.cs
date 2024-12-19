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
    public class CustomerRepository : ICustomerRepository
    {
        IBookOrderDbContext _dbContext;

        public CustomerRepository(IBookOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultDto<bool>> CreateAsync(CustomerCreateDto dto, CancellationToken cancellationToken)
        {
            Customer customer = await _dbContext.Customers.FirstOrDefaultAsync(f => f.Email == dto.Email, cancellationToken);
            if (customer != null)
                return new ResultDto<bool>(false, ResultMessages.CUSTOMER_EMAIL_ALREADY_USING_BY_ANOTHER_USER);


            Customer entity = new Customer
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
            };

            _dbContext.Customers.Add(entity);

            if (await _dbContext.SaveChangesAsync(cancellationToken) > 0)
                return new ResultDto<bool>(true);
            else
                return new ResultDto<bool>(false, ResultMessages.CUSTOMER_CANNOT_CREATE);
        }

        public async Task<ResultDto<List<CustomerDto>>> GetAllAsync(PaginationDto pagination, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Customers
                             .AsNoTracking()
                             .Skip((pagination.Page - 1) * pagination.Size)
                             .Take(pagination.Size)
                             .Select(s => new CustomerDto
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 LastName = s.LastName,
                                 Email = s.Email,
                             })
                             .ToListAsync(cancellationToken);

            return new ResultDto<List<CustomerDto>>(data);
        }
    }
}
