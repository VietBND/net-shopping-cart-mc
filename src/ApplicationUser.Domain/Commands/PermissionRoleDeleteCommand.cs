using ApplicationUser.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.MediatR.Commands;

namespace ApplicationUser.Domain.Commands
{
    public class PermissionRoleDeleteCommand : ICommand<bool>
    {
        public Guid RoleId { get; set; }
        public RolePermissionDto[] RolePermissions { get; set; }
    }
}
