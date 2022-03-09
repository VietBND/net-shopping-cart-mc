using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.RoleAggregate
{
    public class Permission : Entity<Guid>
    {
        public Permission()
        {
            RolePermissionMappings = new HashSet<RolePermissionMapping>();
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<RolePermissionMapping> RolePermissionMappings { get; set; }
    }
}
