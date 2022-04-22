using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Domain.Repositories;
using VietBND.Domain.Uow;
using VietBND.MediatR.Commands;

namespace ApplicationUser.Infrastructures.CommandHandlers
{
    public class PermissionCommandHandler : ICommandHandler<PermissionRoleCreateCommand, bool>, ICommandHandler<PermissionRoleDeleteCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<RolePermissionMapping, Guid> _repository;
        public PermissionCommandHandler(IRepository<RolePermissionMapping, Guid> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(PermissionRoleCreateCommand request, CancellationToken cancellationToken)
        {
            
            foreach (var permissionId in request.PermissionIds)
            {
                await _repository.InsertAsync(new RolePermissionMapping()
                {
                    PermissionId = permissionId,
                    RoleId = request.RoleId,
                    CreatedAt = DateTime.UtcNow
                });
            }
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> Handle(PermissionRoleDeleteCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.RolePermissions.Select(s => s.Id).ToArray());
            return true;
        }
    }
}
