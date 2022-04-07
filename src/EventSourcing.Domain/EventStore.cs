using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Entities;

namespace EventSourcing.Domain
{
    public class EventStore : AuditedEntity<Guid>
    {
        public Guid TransactionId { get; set; }
        public string EventName { get; set; }
        public string Data { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsFailed { get; set; }
    }
}
