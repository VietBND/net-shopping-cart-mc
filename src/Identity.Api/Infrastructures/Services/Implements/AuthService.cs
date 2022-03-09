using Identity.Api.Infrastructures.Domain;
using Identity.Api.Infrastructures.Services.Interfaces;
using Identity.Api.Models.Requests;
using Identity.Api.Models.Responses;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VietBND.Common;
using VietBND.Domain.Repositories;

namespace Identity.Api.Infrastructures.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<ApplicationUser, long> _repository;
        private readonly IConfiguration _configuration;
        public AuthService(IRepository<ApplicationUser, long> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<AccountInfoResponse> Login(AccountLoginRequest request)
        {
            var user = _repository.GetAllList(s => s.UserName == request.Username).SingleOrDefault();
            if (user == null)
            {
                throw new KeyNotFoundException("Account Name is not exist");
            }
            if (!Encryption.Validate(user.Password,request.Password,user.Salt))
            {
                throw new KeyNotFoundException("Password is incorrect");
            }
            var accessToken = generateJwtToken(user);
            return new AccountInfoResponse()
            {
                AccessToken = accessToken,
                Name = user.Name,
                RefToken = accessToken
            };
        }

        private string generateJwtToken(ApplicationUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtBearer:Key"]);
            var secretKey = Encoding.ASCII.GetBytes(_configuration["JwtBearer:SecretKey"]);
            byte[] ecKey = new byte[256 / 8];
            Array.Copy(secretKey, ecKey, 256 / 8);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
