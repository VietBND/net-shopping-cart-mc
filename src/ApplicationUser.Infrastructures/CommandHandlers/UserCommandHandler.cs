using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.Events;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.AspNetCore.Exceptions;
using VietBND.Domain.Repositories;
using VietBND.Domain.Uow;
using VietBND.CQRS;
using ApplicationUser.Domain.UserAggregate;

namespace ApplicationUser.Infrastructures.CommandHandlers
{
    public class UserCommandHandler : ICommandHandler<UserRegisterCommand, long>
    {
        private readonly IRepository<User,long> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserCommandHandler(IRepository<User, long> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<long> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
        {
            //var userExist = _repository.GetAll().Where(s => s.UserName == command.Username).AsNoTracking().Any();
            //if (userExist)
            //{
            //    throw new AppException("User is exist");
            //}
            var entity = _mapper.Map<User>(command);
            entity = await _repository.InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            entity.AddDomainEvent(new AuthRegisterEvent()
            {
                UserId = entity.Id,
                Email = entity.Email,
                Username = entity.UserName,
                Name = entity.Name,
                Password = entity.Password,
                CreatedAt = entity.CreatedAt,
                Salt = entity.Salt,
                CreatedBy = entity.CreatedBy,
                TenantId = entity.TenantId
            });
            await _unitOfWork.DispatchDomainEventsAsync();
            return entity.Id;
        }
    }
}
