using ApplicationUser.Domain.Commands;
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
    public class RolePermissionCommandHandler : AsyncCommandHandler<RolePermissionCreationCommand>
    {
        private readonly IRepository<RolePermissionMapping,Guid> _rolePermissionMappingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RolePermissionCommandHandler(IUnitOfWork unitOfWork, IRepository<RolePermissionMapping, Guid> rolePermissionMappingRepository)
        {
            _unitOfWork = unitOfWork;
            _rolePermissionMappingRepository = rolePermissionMappingRepository;
        }

        protected override async Task Handle(RolePermissionCreationCommand request, CancellationToken cancellationToken)
        {
            foreach (var permissionKey in request.Permissions)
            {
                await _rolePermissionMappingRepository.InsertAsync(new RolePermissionMapping()
                {
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = String.Empty,
                    PermissionId = permissionKey,
                    RoleId = request.RoleId
                });
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
