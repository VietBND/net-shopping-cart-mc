using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.MediatR.Commands;

namespace ApplicationUser.Domain.Commands
{
    public class UserUpdateCommand : ICommand<Guid>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
