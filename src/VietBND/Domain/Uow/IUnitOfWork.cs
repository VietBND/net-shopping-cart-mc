using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.Domain.Uow
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
        void BeginTransaction();
        Task DispatchDomainEventsAsync();
    }
}
