using BookOrder.Business.Dto;
using BookOrder.Business.Repo;
using BookOrder.Business.Repo.Interfaces;
using BookOrder.Helper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace BookOrder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync(CustomerCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await _repository.CreateAsync(dto, cancellationToken);
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
