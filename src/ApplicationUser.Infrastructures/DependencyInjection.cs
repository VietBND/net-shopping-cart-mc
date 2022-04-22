using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUser.Infrastructures
{
    public class DependencyInjection
    {
        public static void AddApplicationUser(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance<>()
        }
    }
}
