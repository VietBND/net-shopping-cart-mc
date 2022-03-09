
using ApplicationUser.Domain.UserAggregate;
using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.RoleAggregate
{
    public class Role : AggregateRoot<Guid>
    {
        public Role()
        {
            RolePermissionMappings = new HashSet<RolePermissionMapping>();
            UserRoleMappings = new HashSet<UserRoleMapping>();
        }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<RolePermissionMapping> RolePermissionMappings { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }
    }
}
