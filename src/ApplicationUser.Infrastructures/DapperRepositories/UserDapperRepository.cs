using ApplicationUser.Domain.Dto;
using ApplicationUser.Domain.Entities;
using ApplicationUser.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Dapper;

namespace ApplicationUser.Infrastructures.DapperRepositories
{
    public interface IUserDapperRepository
    {
        Task<UserDto> GetByUsername(string username);
        Task<IEnumerable<UserDto>> GetByPagination(UserGetByPaginationQuery request);
        Task<IEnumerable<UserDto>> GetByListId(params Guid[] ids);
    }
    public class UserDapperRepository : IUserDapperRepository
    {
        private readonly IDapperBuilder _dapperBuilder;
        public UserDapperRepository(IDapperBuilder dapperBuilder)
        {
            _dapperBuilder = dapperBuilder;
        }

        public async Task<IEnumerable<UserDto>> GetByListId(params Guid[] ids)
        {
            var sqlBuilder = _dapperBuilder.Build<User>();
            sqlBuilder.OrWhere("t1.Id IN (@ids)", new { ids = ids });
            return await _dapperBuilder.GetEnumerable<UserDto>();
        }

        public async Task<IEnumerable<UserDto>> GetByPagination(UserGetByPaginationQuery request)
        {
            var sqlBuilder = _dapperBuilder.Build<User>(request);
            return await _dapperBuilder.GetEnumerable<UserDto>();
        }

        public async Task<UserDto> GetByUsername(string username)
        {
            var sqlBuilder = _dapperBuilder.Build<User>();
            sqlBuilder.OrWhere("t1.Username = @username", new { username = username });
            return await _dapperBuilder.SingleOrDefault<UserDto>();
        }
    }
}
