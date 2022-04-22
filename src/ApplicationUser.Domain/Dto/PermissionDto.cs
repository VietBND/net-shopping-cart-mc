using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUser.Domain.Dto
{
    public class PermissionDto
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}
