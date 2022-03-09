using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;

namespace Identity.Api.Infrastructures.Domain
{
    public class ApplicationUser : Entity<long>
    {
        public ApplicationUser()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
