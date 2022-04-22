using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.Entities
{
    public class User : AuditedEntity<Guid>
    {
        public User()
        {
            UserRoles = new HashSet<UserRoleMapping>();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Username { get; set; }
        public ICollection<UserRoleMapping> UserRoles { get; set; }
    }
}
