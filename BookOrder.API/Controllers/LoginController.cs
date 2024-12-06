using BookOrder.Business.Dto;
using BookOrder.Business.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace BookOrder.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginRepository _repository;

        public LoginController(ILoginRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
        {
            var result = await _repository.LoginAsync(dto, cancellationToken);
            return Ok(result);
        }
    }
}
