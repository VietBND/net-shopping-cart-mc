using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.Domain.Entities
{
    public class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot
    {
        private List<INotification> _domainEvents = null!;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
        public void AddDomainEvent(INotification domainEvents)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(domainEvents);
        }
        public void RemoveDomainEvent(INotification domainEvents)
        {
            _domainEvents.Remove(domainEvents);
        }
        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }
    }
}
