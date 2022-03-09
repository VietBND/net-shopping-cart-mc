using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.RabbitMQ
{
    public interface IRabbitMQConnectionFactory
    {
        IConnection _ProducerConnection { get; }
        IConnection _ConsumeConnection { get; }
    }
}
