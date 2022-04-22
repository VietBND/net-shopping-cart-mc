using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.Entities
{
    public class RolePermissionMapping : AuditedEntity<Guid>
    {
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
        public Permission Permission { get; set; }
        public Guid PermissionId { get; set; }
    }
}
