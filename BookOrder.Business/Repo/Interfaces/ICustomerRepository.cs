using BookOrder.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo.Interfaces
{
    public interface ICustomerRepository
    {
        Task<ResultDto<bool>> CreateAsync(CustomerCreateDto dto, CancellationToken cancellationToken);
        Task<ResultDto<List<CustomerDto>>> GetAllAsync(PaginationDto pagination, CancellationToken cancellationToken);
    }
}
