using Identity.Api.Infrastructures.EventHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.RabbitMQ;
using Identity.Api.Infrastructures.Domain.IntergrationEvents;

namespace Identity.Api.Infrastructures
{
    public static class IdentityConfigureEventBus
    {
        public static void ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<AuthRegisterIntergrationEvent, IIntergrationEventHandler<AuthRegisterIntergrationEvent>>();
        }
    }
}
