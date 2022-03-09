using Autofac;
using Identity.Api.Infrastructures.EventHandlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;
using VietBND.Domain.Repositories;
using VietBND.Domain.Uow;
using VietBND.CQRS;
using VietBND.RabbitMQ;
using Identity.Api.Infrastructures.Domain.IntergrationEvents;
using Identity.Api.Infrastructures.Services.Implements;
using Identity.Api.Infrastructures.Services.Interfaces;

namespace Identity.Api.Infrastructures.Installed
{
    public static class VietBNDFramework 
    {
        public static void AddVietBNDFramework(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(IdentityRepository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency();
            containerBuilder.RegisterType<IdentityDbContext>().As<IDbContext<IdentityDbContext>>().SingleInstance();
            containerBuilder.RegisterType<IdentityUow>().As<IUnitOfWork>().SingleInstance();
            containerBuilder.RegisterType<RabbitMQConnectionFactory>().As<IRabbitMQConnectionFactory>().SingleInstance();
            containerBuilder.RegisterType<RabbitMQBus>().As<IEventBus>().SingleInstance();
            containerBuilder.RegisterType<AuthRegisterIntergrationEventHandler>().As<IIntergrationEventHandler<AuthRegisterIntergrationEvent>>().InstancePerDependency();
        }

        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AuthService>().As<IAuthService>().InstancePerDependency();
        }
    }
}
