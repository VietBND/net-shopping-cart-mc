using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.RabbitMQ
{
    public interface IIntergrationEventHandler<in TEvent> : IIntergrationEventHandler
        where TEvent : IIntergrationEvent
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken);
    }

    public interface IIntergrationEventHandler
    {
    }
}
