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
    public interface IUserDapperRepository
    {
        Task<AccountLoginByUsernameDto> GetByUsername(string username);
        Task<AppUserDto> GetById(Guid userId);
        Task<AccountLoginByUsernameDto> GetByRefreshToken(string refreshToken);
    }
    public class UserDapperRepository : IUserDapperRepository
    {
        private readonly IDapperBuilder _dapperBuilder;
        public UserDapperRepository(IDapperBuilder dapperBuilder)
        {
            _dapperBuilder = dapperBuilder;
        }

        public async Task<AppUserDto> GetById(Guid userId)
        {
            var builder = _dapperBuilder.Build<AppUser>();
            builder
                .Where("t1.Id = @userId", new { userId })
                .Select("t1.Username")
                .Select("t1.Name")
                .Select("t1.Email");
            return await _dapperBuilder.SingleOrDefault<AppUserDto>();
        }

        public async Task<AccountLoginByUsernameDto> GetByUsername(string username)
        {
            var builder = _dapperBuilder.Build<AppUser>();
            builder
                .Where("t1.Username = @username",new { username })
                .Select("t1.Username")
                .Select("t1.Password")
                .Select("t1.Salt")
                .Select("t1.Name")
                .Select("t1.Email");
            return await _dapperBuilder.SingleOrDefault<AccountLoginByUsernameDto>();
        }

        public async Task<AccountLoginByUsernameDto> GetByRefreshToken(string refreshToken)
        {
            var builder = _dapperBuilder.Build<AppUser>();
            builder
                .Join("RefreshTokenMappingUser t2")
                .Where("t2.RefreshToken = @refreshToken", new { refreshToken })
                .Select("t1.Username")
                .Select("t1.Name")
                .Select("t1.Email");
            return await _dapperBuilder.SingleOrDefault<AccountLoginByUsernameDto>();
        }
    }
}
