using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.Tests
{
    public class DependencyInjectionTestBase
    {
        private IServiceProvider _serviceProvider;
        private static readonly object _locker = new object();

        public DependencyInjectionTestBase()
        {
            var serviceCollection = new ServiceCollection();
            this.ServiceCollection = serviceCollection;
        }

        protected IServiceCollection ServiceCollection { get; }

        protected IServiceProvider ServiceProvider
        {
            get
            {
                lock (_locker)
                {
                    if (_serviceProvider == null)
                    {
                        _serviceProvider = this.ServiceCollection.BuildServiceProvider();
                    }

                    return _serviceProvider;
                }
            }
        }

        protected T GetService<T>()
        {
            return this.ServiceProvider.GetRequiredService<T>();
        }

        protected Mock<T> GetMockedService<T>()
            where T : class
        {
            return this.ServiceProvider.GetRequiredService<Mock<T>>();
        }

        protected Lazy<T> GetLazyService<T>()
            where T : class
        {
            return this.ServiceProvider.GetRequiredService<Lazy<T>>();
        }
    }
}
