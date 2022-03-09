using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;

namespace VietBND.Domain.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync<TDbContext>(this IMediator mediator, TDbContext dbContext ) where TDbContext : Microsoft.EntityFrameworkCore.DbContext
        {
            var domainEntities = dbContext.ChangeTracker.Entries<IAggregateRoot>()
                .Where(domainEntity => domainEntity.Entity.DomainEvents != null && domainEntity.Entity.DomainEvents.Any());
            var domainEvents = domainEntities.SelectMany(s => s.Entity.DomainEvents).ToList();
            domainEntities.ToList().ForEach(domainEntity => domainEntity.Entity.ClearDomainEvent());
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
