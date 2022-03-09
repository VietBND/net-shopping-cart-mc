using Identity.Api.Infrastructures.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VietBND.Domain.Repositories;

namespace Identity.Api.Infrastructures.Utils
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(ApplicationUser user);
        public int? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly IRepository<ApplicationUser, long> _userRepository;
        private readonly IRepository<RefreshToken, int> _refreshTokenRepository;
        private readonly IConfiguration _appSettings;

        public JwtUtils(
            IConfiguration appSettings, IRepository<ApplicationUser, long> userRepository, IRepository<RefreshToken, int> refreshTokenRepository)
        {
            _appSettings = appSettings;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            // generate token that is valid for 15 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings["JwtBearer:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings["JwtBearer:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = getUniqueToken(),
                // token is valid for 7 days
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            return refreshToken;

            string getUniqueToken()
            {
                // token is a cryptographically strong random sequence of values
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                // ensure token is unique by checking against db
                var tokenIsUnique = !_userRepository.GetAllList(u => u.RefreshTokens.Any(t => t.Token == token)).Any();

                if (!tokenIsUnique)
                    return getUniqueToken();

                return token;
            }
        }
    }
}
