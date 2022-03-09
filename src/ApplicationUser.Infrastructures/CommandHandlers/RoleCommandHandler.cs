using ApplicationUser.Domain.Commands;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Repositories;
using VietBND.Domain.Uow;
using VietBND.CQRS;
using ApplicationUser.Domain.RoleAggregate;

namespace ApplicationUser.Infrastructures.CommandHandlers
{
    public class RoleCommandHandler : ICommandHandler<RoleCreationCommand, Guid>
    {
        private readonly IRepository<Role,Guid> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public RoleCommandHandler(IRepository<Role, Guid> repository, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(RoleCreationCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Role>(request);
            entity = await _repository.InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (request.Permissions.Count > 0)
            {
                await _mediator.Send(new RolePermissionCreationCommand()
                {
                    Permissions = request.Permissions,
                    RoleId = entity.Id
                });
            }
            return entity.Id;
        }
    }
}
