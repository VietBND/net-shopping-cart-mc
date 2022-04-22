using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.MediatR.Commands;

namespace ApplicationUser.Domain.Commands
{
    public class RoleDeleteCommand : ICommand<bool>
    {
        public Guid[] Ids { get; set; }
    }
}
