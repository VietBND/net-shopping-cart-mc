using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;

namespace ApplicationUser.Domain.Entities
{
    public class Tenant : Entity
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public bool IsActive { get; set; }
    }
}
