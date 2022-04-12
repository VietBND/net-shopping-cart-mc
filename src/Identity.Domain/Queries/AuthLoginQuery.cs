using Identity.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.MediatR.Queries;

namespace Identity.Domain.Queries
{
    public class AuthLoginQuery : IQuery<AccountLoginSuccess>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}
