using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VietBND.AspNetCore;

namespace ApplicationUser.Api.Controllers
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] UserGetByPaginationQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new UserGetByIdQuery() { Id = id});
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreationCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] UsersDeleteCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
