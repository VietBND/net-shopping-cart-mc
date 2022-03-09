using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.CQRS;

namespace ApplicationUser.Domain.Commands
{
    public class RoleDeletionCommand : ICommand
    {
        public IList<Guid> RoleIds { get; set; }
    }
}
