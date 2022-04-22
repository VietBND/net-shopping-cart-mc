using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.Entities;
using ApplicationUser.Infrastructures.DapperRepositories;
using AutoMapper;
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
    public class UserCommandHandler : ICommandHandler<UserCreationCommand, Guid>,ICommandHandler<UserUpdateCommand, Guid>,ICommandHandler<UsersDeleteCommand,bool>
    {
        private readonly IUserDapperRepository _userDapperRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User, Guid> _repository;
        public UserCommandHandler(IRepository<User, Guid> repository, IUserDapperRepository userDapperRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _userDapperRepository = userDapperRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(UserCreationCommand request, CancellationToken cancellationToken)
        {
            var userExist = _userDapperRepository.GetByUsername(request.Username);
            if (userExist != null)
            {
                throw new BadRequestException("Username exist");
            }
            var user = _mapper.Map<User>(request);
            user = await _repository.InsertAsync(user);
            await _unitOfWork.SaveChangeAsync();
            return user.Id;
        }

        public async Task<bool> Handle(UsersDeleteCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Ids);
            return true;
        }

        public async Task<Guid> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.SingleOrDefaultAsync(user => user.Id == request.Id);
            user = _mapper.Map<User>(request);
            if (user == null)
                throw new BadRequestException("User not exist");
            await _repository.UpdateAsync(user);
            await _unitOfWork.SaveChangeAsync();
            return user.Id;
        }
    }
}
