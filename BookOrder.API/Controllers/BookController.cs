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
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BookCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await _repository.CreateAsync(dto, cancellationToken);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] BookUpdateDto dto, CancellationToken cancellationToken)
        {
            var result = await _repository.UpdateAsync(dto, cancellationToken);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDto pagination, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(pagination, cancellationToken);
            return Ok(result);
        }
    }
}
