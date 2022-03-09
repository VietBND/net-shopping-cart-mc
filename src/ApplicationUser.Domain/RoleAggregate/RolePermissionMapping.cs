
using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.RoleAggregate
{
    public class RolePermissionMapping : Entity<Guid>
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
