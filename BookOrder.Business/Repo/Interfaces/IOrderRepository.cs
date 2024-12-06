using BookOrder.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo.Interfaces
{
    public interface IOrderRepository
    {
        Task<ResultDto<List<OrderDetailDto>>> FindOrdersAsync(OrderSearchDto searchDto, CancellationToken cancellationToken);
        Task<ResultDto<bool>> OrderBookAsync(OrderCreateDto dto, CancellationToken cancellationToken);
    }
}
