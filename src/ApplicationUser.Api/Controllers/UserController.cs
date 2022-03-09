using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.Models.Requests;
using ApplicationUser.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VietBND.AspNetCore;

namespace Dms.ApplicationUser.Controllers
{

    [ApiController]
    [Route("api/user")]
    public class UserController : ShoppingCartControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long userId)
        {
            return Ok();
        }

        
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserCreationRequest request)
        {
            var response = await _service.Create(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(long id,[FromBody] UserUpdationRequest request)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int[] ids)
        {
            return Ok();
        }
    }
}
