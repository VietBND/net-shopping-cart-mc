using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.Entities
{
    public class Role : AuditedEntity<Guid>
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermissionMapping>();
            UserRoles = new HashSet<UserRoleMapping>();
        }
        public string Name { get;set; }
        public bool IsActive { get; set; }
        public ICollection<RolePermissionMapping> RolePermissions { get; set; }
        public ICollection<UserRoleMapping> UserRoles { get; set; }
    }
}
