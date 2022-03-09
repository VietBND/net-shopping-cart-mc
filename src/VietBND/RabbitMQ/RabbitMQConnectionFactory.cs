using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.RabbitMQ
{
    public sealed class RabbitMQConnectionFactory : IRabbitMQConnectionFactory
    {
        public IConnection _ProducerConnection { get; }
        public IConnection _ConsumeConnection { get; }
        public static readonly object _object = new object();
        public RabbitMQConnectionFactory()
        {
            lock (_object)
            {
                if(_ProducerConnection == null)
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = "host.docker.internal",
                        Port = 5672,
                        UserName = "shoppingcart",
                        Password = "Dangviet12"
                    };
                    _ProducerConnection = factory.CreateConnection();
                }
                if(_ConsumeConnection == null)
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = "host.docker.internal",
                        Port = 5672,
                        UserName = "shoppingcart",
                        Password = "Dangviet12",
                        DispatchConsumersAsync = true,
                        
                    };
                    _ConsumeConnection = factory.CreateConnection();
                }
            }
        }
    }
}
