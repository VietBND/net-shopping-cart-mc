using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.CQRS;

namespace ApplicationUser.Domain.Commands
{
    public class RolePermissionCreationCommand : ICommand
    {
        public Guid RoleId { get; set; }
        public IList<Guid> Permissions { get; set; } = new List<Guid>();
    }
}
