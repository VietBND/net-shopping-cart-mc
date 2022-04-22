using ApplicationUser.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Dapper;

namespace ApplicationUser.Infrastructures.DapperRepositories
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<RolePermissionDto>> GetByRoleId(Guid roleId);
    }
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly IDapperBuilder _dapperBuilder;
        public RolePermissionRepository(IDapperBuilder dapperBuilder)
        {
            _dapperBuilder = dapperBuilder;
        }

        public async Task<IEnumerable<RolePermissionDto>> GetByRoleId(Guid roleId)
        {
            var sqlBuilder = _dapperBuilder.Build<RolePermissionDto>();
            sqlBuilder.OrWhere("t1.RoleId = @roleId", new { roleId = roleId });
            return await _dapperBuilder.GetEnumerable<RolePermissionDto>();
        }
    }
}
