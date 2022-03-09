using ApplicationUser.Domain.RoleAggregate;
using ApplicationUser.Domain.UserAggregate;
using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.UserAggregate
{
    public class UserRoleMapping : Entity<Guid>
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
