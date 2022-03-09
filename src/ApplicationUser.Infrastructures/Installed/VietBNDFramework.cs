using Autofac;
using Identity.Infrastructures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.CQRS;
using VietBND.DbContext;
using VietBND.Domain.Repositories;
using VietBND.Domain.Uow;
using VietBND.RabbitMQ;

namespace ApplicationUser.Infrastructures.Installed
{
    public static class VietBNDFramework 
    {
        public static void AddVietBNDFramework(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(ApplicationUserRepository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            containerBuilder.RegisterType<ApplicationUserDbContext>().As<IDbContext<ApplicationUserDbContext>>().SingleInstance();
            containerBuilder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency();
            containerBuilder.RegisterType<ApplicationUserUow>().As<IUnitOfWork>().SingleInstance();
            containerBuilder.RegisterType<RabbitMQConnectionFactory>().As<IRabbitMQConnectionFactory>().SingleInstance();
            containerBuilder.RegisterType<RabbitMQBus>().As<IEventBus>().SingleInstance();
        }
    }
}
