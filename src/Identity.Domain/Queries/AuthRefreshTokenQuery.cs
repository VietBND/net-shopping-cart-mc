using Identity.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.MediatR.Queries;

namespace Identity.Domain.Queries
{
    public class AuthRefreshTokenQuery : IQuery<AccountLoginSuccess>
    {
        public string RefreshToken { get; set; }
    }
}
