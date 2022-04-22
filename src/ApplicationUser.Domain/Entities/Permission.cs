using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.Entities
{
    public class Permission : AuditedEntity<Guid>
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermissionMapping>();
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RolePermissionMapping> RolePermissions { get; set; }
    }
}
