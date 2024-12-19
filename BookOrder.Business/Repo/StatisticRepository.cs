using BookOrder.Business.Dto;
using BookOrder.Business.Repo.Interfaces;
using BookOrder.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo
{
    public class StatisticRepository : IStatisticRepository
    {
        IBookOrderDbContext _dbContext;

        public StatisticRepository(IBookOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultDto<StatisticDto>> GetStatisticAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Orders
                                   .AsNoTracking()
                                   .Where(w => w.CustomerId == customerId)
                                   .ToListAsync(cancellationToken);

            if (data.Count == 0)
                return new ResultDto<StatisticDto>(ResultMessages.STATISTIC_CANNOT_FIND_ANY_DATA);

            var result = await _dbContext.Orders
                                   .AsNoTracking()
                                   .Where(w => w.CustomerId == customerId)
                                   .GroupBy(w => w.OrderDate.Month + "-" + w.OrderDate.Year)
                                   .Select(s => new StatisticDto
                                   {
                                       NameOfMonth = s.Key,
                                       TotalOrderCount = s.Count(),
                                       TotalBookCount = s.Sum(m => m.Quantity),
                                       TotalPurchasedAmount = s.Sum(m => m.TotalAmount),
                                   })
                                   .FirstOrDefaultAsync(cancellationToken);

            return new ResultDto<StatisticDto>(result);
        }
    }
}
