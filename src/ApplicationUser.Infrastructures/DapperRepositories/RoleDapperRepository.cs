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
    public interface IRoleDapperRepository
    {
        Task<IEnumerable<RoleDto>> GetByPagination(RoleGetByPaginationQuery request);
        Task<IEnumerable<RoleDto>> GetByListId(params string[] ids);
        Task<RoleDto> GetByName(string name);
    }
    public class RoleDapperRepository : IRoleDapperRepository
    {
        private readonly IDapperBuilder _dapperBuilder;
        public RoleDapperRepository(IDapperBuilder dapperBuilder)
        {
            _dapperBuilder = dapperBuilder;
        }
        public async Task<IEnumerable<RoleDto>> GetByPagination(RoleGetByPaginationQuery request)
        {
            var sqlBuilder = _dapperBuilder.Build<Role>(request);
            return await _dapperBuilder.GetEnumerable<RoleDto>();
        }

        public async Task<IEnumerable<RoleDto>> GetByListId(params string[] ids)
        {
            var sqlBuilder = _dapperBuilder.Build<Role>();
            sqlBuilder.OrWhere("t1.Id IN (@ids)", new { ids = ids });
            return await _dapperBuilder.GetEnumerable<RoleDto>();
        }

        public async Task<RoleDto> GetByName(string name)
        {
            var sqlBuilder = _dapperBuilder.Build<Role>();
            sqlBuilder.OrWhere("t1.Name = @Name", new { Name = name });
            return await _dapperBuilder.SingleOrDefault<RoleDto>();
        }
    }
}
