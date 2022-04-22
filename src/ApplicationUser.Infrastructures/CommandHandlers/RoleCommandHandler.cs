using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.Dto;
using ApplicationUser.Domain.Entities;
using ApplicationUser.Infrastructures.DapperRepositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.AspNetCore.Exceptions;
using VietBND.Domain.Repositories;
using VietBND.Domain.Uow;
using VietBND.MediatR.Commands;

namespace ApplicationUser.Infrastructures.CommandHandlers
{
    public class RoleCommandHandler : ICommandHandler<RoleCreationCommand, Guid>, 
        ICommandHandler<RoleUpdateCommand, Guid>,
        ICommandHandler<RoleDeleteCommand, bool>
    {
        private readonly IRoleDapperRepository _roleDapperRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IPermissionDapperRepository _permissionDapperRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Role, Guid> _repository;
        public RoleCommandHandler(IRepository<Role, Guid> repository, IRoleDapperRepository roleDapperRepository, IMapper mapper, IUnitOfWork unitOfWork, IPermissionDapperRepository permissionDapperRepository, IMediator mediator, IRolePermissionRepository rolePermissionRepository)
        {
            _repository = repository;
            _roleDapperRepository = roleDapperRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _permissionDapperRepository = permissionDapperRepository;
            _mediator = mediator;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<Guid> Handle(RoleCreationCommand request, CancellationToken cancellationToken)
        {
            var roleExist = _roleDapperRepository.GetByName(request.Name);
            if (roleExist != null)
            {
                throw new BadRequestException("Role Name exist");
            }
            using (var transaction = _unitOfWork.GetDatabase().BeginTransaction())
            {
                var role = _mapper.Map<Role>(request);
                role = await _repository.InsertAsync(role);
                var permission = await _permissionDapperRepository.GetByListKey(request.Permissions);
                await _mediator.Send(new PermissionRoleCreateCommand()
                {
                    PermissionIds = permission.Select(s => s.Id).ToArray(),
                    RoleId = role.Id
                });
                transaction.Commit();
                await _unitOfWork.SaveChangeAsync();
                return role.Id;
            }
        }

        public async Task<bool> Handle(RoleDeleteCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Ids);
            return true;
        }

        public async Task<Guid> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.SingleOrDefaultAsync(role => role.Id == request.Id);
            role = _mapper.Map<Role>(request);
            if (role == null)
                throw new BadRequestException("Role not exist");
            using (var transaction = _unitOfWork.GetDatabase().BeginTransaction())
            {
                try
                {
                    await _repository.UpdateAsync(role);
                    var permissionRequest = await _permissionDapperRepository.GetByListKey(request.Permissions);
                    var permissionsByRoleId = await _rolePermissionRepository.GetByRoleId(request.Id);
                    var permissionIdsToDelete = permissionsByRoleId.Select(s => s.PermissionId).Except(permissionRequest.Select(s => s.Id));
                    var permissionToDelete = permissionsByRoleId.Where(s => permissionIdsToDelete.Contains(s.PermissionId));
                    var permissionIdToCreate = permissionRequest.Select(s => s.Id).Except(permissionsByRoleId.Select(s => s.PermissionId));
                    await _mediator.Send(new PermissionRoleDeleteCommand()
                    {
                        RolePermissions = permissionToDelete.ToArray(),
                        RoleId = role.Id
                    });
                    await _mediator.Send(new PermissionRoleCreateCommand()
                    {
                        PermissionIds = permissionIdToCreate.ToArray(),
                        RoleId = role.Id
                    });
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
                finally
                {
                    await _unitOfWork.SaveChangeAsync();
                }
                return role.Id;
            }
        }
    }
}
