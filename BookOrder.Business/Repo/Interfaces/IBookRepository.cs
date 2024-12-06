using BookOrder.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo.Interfaces
{
    public interface IBookRepository
    {
        Task<ResultDto<bool>> CreateAsync(BookCreateDto dto, CancellationToken cancellationToken);
        Task<ResultDto<bool>> UpdateAsync(BookUpdateDto dto, CancellationToken cancellationToken);
        Task<ResultDto<List<BookDto>>> GetAllAsync(PaginationDto pagination, CancellationToken cancellationToken);
    }
}
