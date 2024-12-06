using BookOrder.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo.Interfaces
{
    public interface ILoginRepository
    {
        public Task<ResultDto<string>> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
    }
}
