using Identity.Domain.Dto;
using Identity.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VietBND.AspNetCore;

namespace Identity.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthLoginQuery request)
        {
            var response = await _mediator.Send<AccountLoginSuccess>(request);
            return Ok(response);
        }
    }
}
