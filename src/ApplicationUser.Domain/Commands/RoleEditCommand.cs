using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.CQRS;

namespace ApplicationUser.Domain.Commands
{
    public class RoleEditCommand : ICommand
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
        public IList<Guid> Permissions { get; set; } = new List<Guid>();
    }
}
