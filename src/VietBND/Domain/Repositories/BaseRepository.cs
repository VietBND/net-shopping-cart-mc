using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace VietBND.Domain.Repositories
{
    public abstract class EfCoreRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public virtual int Count()
        {
            return GetAll().AsNoTracking().Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().AsNoTracking().Count(predicate);
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public abstract void Delete(TEntity entity);

        public abstract void Delete(TPrimaryKey id);

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = GetAll().Where(predicate).ToArray();
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public virtual Task DeleteAsync(TEntity entity)
        {

            return Task.Run(() => Delete(entity));
        }

        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            return Task.Run(() => Delete(id));
        }

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() => Delete(predicate));
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id))!;
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate)!;
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            return GetAll().Where(CreateEqualityExpressionForId(id)).SingleOrDefault()!;
        }

        public abstract IQueryable<TEntity> GetAll();
        public abstract Task<IQueryable<TEntity>> GetAllAsync();

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAll();
        }

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().AsNoTracking().ToList();
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).AsNoTracking().ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        public virtual Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return Task.FromResult(Get(id));
        }

        public abstract TEntity Insert(TEntity entity);

        public virtual TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            if (entity.IsTransient())
                return Insert(entity);
            return Update(entity);
        }

        public virtual TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            return InsertOrUpdate(entity).Id;
        }

        public virtual Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdateAndGetId(entity));
        }

        public virtual Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdate(entity));
        }

        public virtual TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }

        public virtual long LongCount()
        {
            return GetAll().AsNoTracking().LongCount();
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().AsNoTracking().LongCount(predicate);
        }

        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll().AsNoTracking());
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public abstract TEntity Update(TEntity entity);

        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return Update(entity);

        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = Get(id);
            updateAction(entity).Wait();
            return Task.FromResult(Update(entity));
        }
        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            var idValue = Convert.ChangeType(id, typeof(TPrimaryKey));

            Expression<Func<object>> closure = () => idValue!;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForIsDeleted()
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "IsDeleted");

            Expression<Func<object>> closure = () => false;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
