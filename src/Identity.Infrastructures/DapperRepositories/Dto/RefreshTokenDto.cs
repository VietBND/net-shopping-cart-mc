using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructures.DapperRepositories.Dto
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string CreatedByIp { get; set; }
        public Guid UserId { get; set; }
    }
}
