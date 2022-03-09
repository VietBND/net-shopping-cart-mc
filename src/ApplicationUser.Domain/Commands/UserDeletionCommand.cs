using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.CQRS;

namespace ApplicationUser.Domain.Commands
{
    public class UserDeletionCommand : ICommand
    {
        public IList<Guid> UserIds { get; set; }
    }
}
