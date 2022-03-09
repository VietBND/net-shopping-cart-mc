
using ApplicationUser.Domain.UserAggregate;
using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.UserAggregate
{
    public class User : AggregateRoot<long>
    {
        public User()
        {
            UserRoleMappings = new HashSet<UserRoleMapping>();
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }
    }
}
