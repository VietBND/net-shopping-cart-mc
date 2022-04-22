using Identity.Domain.Entities;
using Identity.Infrastructures.DapperRepositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Dapper;

namespace Identity.Infrastructures.DapperRepositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenDto> GetByRefreshToken(string refreshToken);
    }

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IDapperBuilder _dapperBuilder;
        public RefreshTokenRepository(IDapperBuilder dapperBuilder)
        {
            _dapperBuilder = dapperBuilder;
        }

        public async Task<RefreshTokenDto> GetByRefreshToken(string refreshToken)
        {
            var builder = _dapperBuilder.Build<RefreshTokenUserMapping>();
            builder
                .Where("t1.Token = @refreshToken", new { refreshToken })
                .Select("t1.Token")
                .Select("t1.UserId")
                .Select("t1.Expires")
                .Select("t1.CreatedByIp");
            return await _dapperBuilder.SingleOrDefault<RefreshTokenDto>();
        }

    }
}
