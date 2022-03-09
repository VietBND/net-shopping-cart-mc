using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.CQRS;

namespace VietBND.RabbitMQ
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly ILogger<RabbitMQBus> _logger;
        private readonly IRabbitMQConnectionFactory _rabbitMQConnectionFactory;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _scope;

        public RabbitMQBus(IServiceScopeFactory scope, ILogger<RabbitMQBus> logger, IRabbitMQConnectionFactory rabbitMQConnectionFactory)
        {
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _scope = scope;
            _logger = logger;
            _rabbitMQConnectionFactory = rabbitMQConnectionFactory;
        }
        public void Pushlish<T>(T @event) where T : IIntergrationEvent
        {
            
            using var channel = _rabbitMQConnectionFactory._ProducerConnection.CreateModel();
            var eventName = @event.GetType().Name;
            channel.QueueDeclare(eventName, false, false, false, null);
            var messages = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(messages);
            channel.BasicPublish("", eventName, null, body);
            _logger.LogInformation($"Published from {eventName} with value: {messages}");
        }

        public void Subscribe<T, TH>()
            where T : IIntergrationEvent
            where TH : IIntergrationEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);
            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));
            }
            _handlers[eventName].Add(handlerType);
            StartBasicComsume<T>();
        }

        private void StartBasicComsume<T>() where T : IIntergrationEvent
        {
            IModel channel = _rabbitMQConnectionFactory._ConsumeConnection.CreateModel();
            var eventName = typeof(T).Name;
            channel.QueueDeclare(eventName, false, false, false, null);
            var consume = new AsyncEventingBasicConsumer(channel);
            consume.Received += Consume_Received;
            channel.BasicConsume(eventName, true, consume);
        } 

        private async Task Consume_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());
            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Event {eventName} corrupted with value {message}: {ex.Message}");
                throw;
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {

                using (var scope = _scope.CreateScope())
                {
                    var subcriptions = _handlers[eventName];
                    foreach (var subcription in subcriptions)
                    {
                        var handler = scope.ServiceProvider.GetService(subcription);
                        if (handler == null) continue;
                        var eventType = _eventTypes.SingleOrDefault(s => s.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        var conreteType = typeof(IIntergrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)conreteType.GetMethod("Handle").Invoke(handler, new object[] { @event,CancellationToken.None });
                    }
                    _logger.LogInformation($"Subcriber from {eventName} with value: {message}");
                }

            }
        }
    }
}
