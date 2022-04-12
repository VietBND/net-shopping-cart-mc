using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain;
using VietBND.Domain.Repositories;

namespace EventSourcing.Infrastructures.Repositories
{
    public interface IEventRepository<TEntity, TPrimaryKey> : IRepository<TEntity,TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
    }
}
