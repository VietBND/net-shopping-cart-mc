using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VietBND.AspNetCore;

namespace ApplicationUser.Api.Controllers
{

    [ApiController]
    [Route("api/roles")]
    public class RoleController : ShoppingCartControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long roleId)
        {
            return Ok();
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RoleCreationRequest request)
        {
            var roleId = await _mediator.Send(new RoleCreationCommand()
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                Permissions = request.Permissions
            });
            return Ok(roleId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(long userId, [FromBody] UserUpdationRequest request)
        {
            
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            return Ok();
        }
    }
}
