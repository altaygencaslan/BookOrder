using BookOrder.Business.Dto;
using BookOrder.Business.Repo;
using BookOrder.Business.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace BookOrder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatisticController : Controller
    {
        private readonly IStatisticRepository _repository;

        public StatisticController(IStatisticRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] Guid customerId, CancellationToken cancellationToken)
        {
            var result = await _repository.GetStatisticAsync(customerId, cancellationToken);
            return Ok(result);
        }
    }
}
