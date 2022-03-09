using ApplicationUser.Services.Implements;
using ApplicationUser.Services.Interfaces;
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
    public static class RegisterServices 
    {
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}
