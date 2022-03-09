using Identity.Api.Filters;
using Identity.Api.Infrastructures.Services.Interfaces;
using Identity.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using VietBND.AspNetCore;

namespace Identity.Api.Controllers
{
    [Route("/api/auth")]
    public class AuthController : ShoppingCartControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AccountLoginRequest request)
        {
            var response = await _service.Login(request);
            return Ok(response);
        }

        [HttpPost("get-info")]
        [Authorize]
        public async Task<IActionResult> GetInfo()
        {
            return Ok();
        }
    }
}
