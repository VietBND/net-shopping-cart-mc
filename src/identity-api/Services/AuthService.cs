using Grpc.Core;
using Identity.Api;

namespace Identity.Api.Services
{
    public class AuthService : Auth.AuthBase
    {
        public AuthService()
        {

        }
        public override Task<HelloReply> Login(AuthLoginRequest request, ServerCallContext context)
        {
            return base.Login(request, context);
        }
    }
}
