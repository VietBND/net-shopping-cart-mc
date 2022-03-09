using Identity.Api.Models.Requests;
using Identity.Api.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Api.Infrastructures.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AccountInfoResponse> Login(AccountLoginRequest request);
    }
}
