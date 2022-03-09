using AutoMapper;
using Identity.Api.Infrastructures.Domain;
using Identity.Api.Infrastructures.Domain.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Repositories;
using VietBND.Domain.Uow;
using VietBND.RabbitMQ;

namespace Identity.Api.Infrastructures.EventHandlers
{
    public class AuthRegisterIntergrationEventHandler : IIntergrationEventHandler<AuthRegisterIntergrationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Infrastructures.Domain.ApplicationUser, long> _repository;
        private readonly IMapper _mapper;

        public AuthRegisterIntergrationEventHandler(IRepository<Infrastructures.Domain.ApplicationUser, long> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(AuthRegisterIntergrationEvent notification, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Infrastructures.Domain.ApplicationUser>(notification);
            await _repository.InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
