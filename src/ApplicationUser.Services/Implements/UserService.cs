using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.Models.Requests;
using ApplicationUser.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUser.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<long> Create(UserCreationRequest request)
        {
            var userId = await _mediator.Send(new UserRegisterCommand()
            {
                Password = request.Password,
                Username = request.Username,
                Email = request.Email,
                Name = request.Name
            });
            return userId;
        }
    }
}
