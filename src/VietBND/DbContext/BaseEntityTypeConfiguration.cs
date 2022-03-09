using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;

namespace VietBND.DbContext
{
    public class BaseEntityTypeConfiguration<TEntity,TPrimaryKey> : IEntityTypeConfiguration<TEntity> where TEntity : Entity<TPrimaryKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(s => s.Id);
        }
    }

    public class BaseAggregateRootTypeConfiguration<TEntity, TPrimaryKey> : IEntityTypeConfiguration<TEntity> where TEntity : AggregateRoot<TPrimaryKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Ignore(s => s.DomainEvents);
        }
    }
}
