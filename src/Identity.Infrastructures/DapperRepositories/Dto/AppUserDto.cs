using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructures.DapperRepositories.Dto
{
    public class AppUserDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
