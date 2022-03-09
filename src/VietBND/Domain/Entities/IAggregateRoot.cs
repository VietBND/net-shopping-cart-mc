using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.Domain.Entities
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
        void AddDomainEvent(INotification domainEvents);
        void RemoveDomainEvent(INotification domainEvents);
        void ClearDomainEvent();
    }
}
