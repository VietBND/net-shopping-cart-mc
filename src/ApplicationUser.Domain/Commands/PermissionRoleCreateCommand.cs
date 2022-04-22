using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.MediatR.Commands;

namespace ApplicationUser.Domain.Commands
{
    public class PermissionRoleCreateCommand : ICommand<bool>
    {
        public Guid[] PermissionIds { get; set; }
        public Guid RoleId { get; set; }
    }
}
