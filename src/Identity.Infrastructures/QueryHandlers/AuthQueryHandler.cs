using Identity.Domain.Dto;
using Identity.Domain.Entities;
using Identity.Domain.Queries;
using Identity.Infrastructures.DapperRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingCart.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VietBND.AspNetCore.Exceptions;
using VietBND.Domain.Repositories;
using VietBND.MediatR.Queries;

namespace Identity.Infrastructures.QueryHandlers
{
    public class AuthQueryHandler : IQueryHandler<AuthLoginQuery, AccountLoginSuccess>,IQueryHandler<AuthRefreshTokenQuery, AccountLoginSuccess>
    {
        private readonly IUserDapperRepository _repository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRepository<RefreshTokenUserMapping, Guid> _refreshTokenCrudRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        public AuthQueryHandler(IUserDapperRepository repository, IConfiguration configuration, IHttpContextAccessor httpContext, IRepository<RefreshTokenUserMapping, Guid> refreshTokenCrudRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _repository = repository;
            _configuration = configuration;
            _httpContext = httpContext;
            _refreshTokenCrudRepository = refreshTokenCrudRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<AccountLoginSuccess> Handle(AuthLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByUsername(request.UserName);
            if (user == null)
            {
                throw new NotFoundException("Username or Password is incorrect");
            }
            if (!Encryption.Validate(user.Password, request.Password, user.Salt))
            {
                throw new NotFoundException("Username or Password is incorrect");
            }
            var refreshToken = generateRefreshToken(ipAddress());
            setTokenCookie(refreshToken.Token);
            await _refreshTokenCrudRepository.InsertAsync(new RefreshTokenUserMapping()
            {
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddMinutes(15),
                Token = refreshToken.Token,
                CreatedAt = DateTime.UtcNow,
            });
            var token = generateJwtBearer(user.Id, refreshToken.Token);
            setTokenCookie(refreshToken.Token);
            return new AccountLoginSuccess()
            {
                AccessToken = token,
                AccountInfo = new AccountInfo()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name,
                },
            };
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            _httpContext.HttpContext.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (_httpContext.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                return _httpContext.HttpContext.Request.Headers["X-Forwarded-For"];
            else
                return _httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        private string generateJwtBearer(Guid userId,string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(10),
                Subject = new ClaimsIdentity(new[] { new Claim("userId", userId.ToString()), new Claim("refreshToken", refreshToken) }),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshTokenUserMapping generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.Encrypt(randomBytes,false);
                return new RefreshTokenUserMapping
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        public async Task<AccountLoginSuccess> Handle(AuthRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByRefreshToken(request.RefreshToken);
            if (user == null) 
                throw new UnauthorizeException("Unauthorize");
            var refreshToken = await _refreshTokenRepository.GetByRefreshToken(request.RefreshToken);
            if (refreshToken.Expires < DateTime.UtcNow)
                throw new UnauthorizeException("Unauthorize");
            var newRefreshToken = generateRefreshToken(ipAddress());
            newRefreshToken.UserId = user.Id;
            await _refreshTokenCrudRepository.InsertAsync(newRefreshToken);
            var jwt = generateJwtBearer(user.Id,newRefreshToken.Token);

            return new AccountLoginSuccess()
            {
                AccessToken = jwt,
                AccountInfo = new AccountInfo()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                }
            };
        }

        //public AuthenticateResponse RefreshToken(string token, string ipAddress)
        //{
        //    var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

        //    // return null if no user found with token
        //    if (user == null) return null;

        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        //    // return null if token is no longer active
        //    if (!refreshToken.IsActive) return null;

        //    // replace old refresh token with a new one and save
        //    var newRefreshToken = generateRefreshToken(ipAddress);
        //    refreshToken.Revoked = DateTime.UtcNow;
        //    refreshToken.RevokedByIp = ipAddress;
        //    refreshToken.ReplacedByToken = newRefreshToken.Token;
        //    user.RefreshTokens.Add(newRefreshToken);
        //    _context.Update(user);
        //    _context.SaveChanges();

        //    // generate new jwt
        //    var jwtToken = generateJwtToken(user);

        //    return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        //}

        //public bool RevokeToken(string token, string ipAddress)
        //{
        //    var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

        //    // return false if no user found with token
        //    if (user == null) return false;

        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        //    // return false if token is not active
        //    if (!refreshToken.IsActive) return false;

        //    // revoke token and save
        //    refreshToken.Revoked = DateTime.UtcNow;
        //    refreshToken.RevokedByIp = ipAddress;
        //    _context.Update(user);
        //    _context.SaveChanges();

        //    return true;
        //}
    }
}
