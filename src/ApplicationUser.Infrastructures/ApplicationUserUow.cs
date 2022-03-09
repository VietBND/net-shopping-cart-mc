using ApplicationUser.Infrastructures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;
using VietBND.Domain.Extensions;
using VietBND.Domain.Uow;

namespace Identity.Infrastructures
{
    public class ApplicationUserUow : IUnitOfWork
    {
        private readonly IDbContext<ApplicationUserDbContext> _dbContext;
        private readonly IMediator _mediator;

        public ApplicationUserUow(IDbContext<ApplicationUserDbContext> dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _dbContext.Instance.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.Instance.SaveChangesAsync();
            
        }

        public async Task DispatchDomainEventsAsync()
        {
            await _mediator.DispatchDomainEventsAsync(_dbContext.Instance);
        }
    }
}
