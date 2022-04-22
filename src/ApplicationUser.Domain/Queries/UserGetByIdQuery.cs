using ApplicationUser.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.MediatR.Queries;

namespace ApplicationUser.Domain.Queries
{
    public class UserGetByIdQuery : IQuery<UserDto>
    {
        public Guid Id { get; set; }
    }
}
