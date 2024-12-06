using BookOrder.Business.Dto;
using BookOrder.Business.Repo;
using BookOrder.Business.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Threading;

namespace BookOrder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> OrderAsync([FromBody] OrderCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await _repository.OrderBookAsync(dto, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromBody] OrderSearchDto dto, CancellationToken cancellationToken)
        {
            var result = await _repository.FindOrdersAsync(dto, cancellationToken);
            return Ok(result);
        }

    }
}
