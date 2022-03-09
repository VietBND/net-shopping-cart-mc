using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUser.Domain.Models.Requests
{
    public class RoleCreationRequest : Request
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
        public IList<Guid> Permissions { get; set; }
    }
}
