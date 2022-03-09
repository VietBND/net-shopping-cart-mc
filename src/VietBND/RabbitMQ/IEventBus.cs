using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.CQRS;

namespace VietBND.RabbitMQ
{
    public interface IEventBus
    {
        void Pushlish<T>(T @event) where T : IIntergrationEvent;
        void Subscribe<T, TH>()
            where T : IIntergrationEvent
            where TH : IIntergrationEventHandler<T>;
    }
}
