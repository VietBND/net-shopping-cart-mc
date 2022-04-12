using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain;
using VietBND.Domain.Entities;

namespace Identity.Domain.Entities
{
    public class ActionLog : Entity<Guid>
    {
        public string Action { get; set; }
        public string Description { get; set; }
    }
}
