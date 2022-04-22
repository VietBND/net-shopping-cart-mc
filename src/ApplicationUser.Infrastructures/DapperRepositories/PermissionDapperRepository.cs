using ApplicationUser.Domain.Dto;
using ApplicationUser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Dapper;

namespace ApplicationUser.Infrastructures.DapperRepositories
{
    public interface IPermissionDapperRepository
    {
        Task<IEnumerable<PermissionDto>> GetAll();
        Task<IEnumerable<PermissionDto>> GetByListKey(params string[] keys);
    }
    public class PermissionDapperRepository : IPermissionDapperRepository
    {
        private readonly IDapperBuilder _dapperBuilder;
        public PermissionDapperRepository(IDapperBuilder dapperBuilder)
        {
            _dapperBuilder = dapperBuilder;
        }

        public async Task<IEnumerable<PermissionDto>> GetByListKey(params string[] keys)
        {
            var sqlBuilder = _dapperBuilder.Build<Permission>();
            sqlBuilder.OrWhere("t1.Key IN (@keys)", new { keys = keys });
            return await _dapperBuilder.GetEnumerable<PermissionDto>();
        }

        public async Task<IEnumerable<PermissionDto>> GetAll()
        {
            var sqlBuilder = _dapperBuilder.Build<Permission>();
            return await _dapperBuilder.GetEnumerable<PermissionDto>();
        }
    }
}
