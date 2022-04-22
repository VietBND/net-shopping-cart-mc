using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.MediatR.Commands;

namespace ApplicationUser.Domain.Commands
{
    public class RoleUpdateCommand : ICommand<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Permissions { get; set; }
    }
}
