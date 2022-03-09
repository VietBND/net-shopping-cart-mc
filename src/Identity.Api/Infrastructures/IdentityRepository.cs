using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;
using VietBND.Domain.Entities;
using VietBND.Domain.Repositories;

namespace Identity.Api.Infrastructures
{
    public class IdentityRepository<TEntity, TPrimaryKey> :
        EfCoreRepository<TEntity, TPrimaryKey>,
        IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IDbContext<IdentityDbContext> _dbContext;
        private readonly DbSet<TEntity> _table;
        public IdentityRepository(IDbContext<IdentityDbContext> dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Instance.Set<TEntity>();
        }
        public override void Delete(TEntity entity)
        {
            var data = _table.Find(entity.Id);
            if (data == null)
            {
                throw new ApplicationException("Id don't exist");
            }
            data.GetType()?.GetProperty("IsDeleted")?.SetValue(data, true);
            _table.Update(data);
        }

        public override void Delete(TPrimaryKey id)
        {
            var data = _table.Find(id);
            if (data == null)
            {
                throw new ApplicationException("Id don't exist");
            }
            data.GetType()?.GetProperty("IsDeleted")?.SetValue(data, true);
            _table.Update(data);
        }

        public override IQueryable<TEntity> GetAll()
        {
            return _table.Where(CreateEqualityExpressionForIsDeleted()).AsQueryable();
        }

        public override async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(_table.Where(CreateEqualityExpressionForIsDeleted()).AsQueryable());
        }

        public override TEntity Insert(TEntity entity)
        {
            var result = _table.Add(entity);
            return result.Entity;
        }

        public override TEntity Update(TEntity entity)
        {
            var result = _table.Update(entity);
            return result.Entity;
        }
    }
}
