using ApplicationUser.Domain.Events;
using ApplicationUser.Domain.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.CQRS;
using VietBND.RabbitMQ;

namespace ApplicationUser.Infrastructures.EventHandlers
{
    public class AuthEventHandler : IEventHandler<AuthRegisterEvent>
    {
        private readonly IEventBus _eventBus;
        public AuthEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task Handle(AuthRegisterEvent notification, CancellationToken cancellationToken)
        {
            _eventBus.Pushlish(new AuthRegisterIntergrationEvent()
            {
                 Name = notification.Name,
                 CreatedAt = notification.CreatedAt,
                 CreatedBy = notification.CreatedBy,
                 Email = notification.Email,
                 Password = notification.Password,
                 Salt = notification.Salt,
                 TenantId = notification.TenantId,
                 UserId = notification.UserId,
                 Username = notification.Username
            });
        }
    }
}
