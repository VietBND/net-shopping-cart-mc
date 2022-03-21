using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB.Domain.Repositories
{
    public abstract class Repository<TEntity, TPrimaryKey> : IRepository<TEntity,TPrimaryKey>
    {
    }
}
