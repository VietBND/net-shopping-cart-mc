using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUser.Infrastructures.MapperConfigurations
{
    public class UserMapperConfiguration : Profile
    {
        public UserMapperConfiguration()
        {
            CreateMap<UserCreationCommand, User>().AfterMap((des,src) =>
            {
                src.CreatedAt = DateTime.UtcNow;
                src.Id = Guid.NewGuid();
            });
            CreateMap<UserUpdateCommand, User>().AfterMap((des, src) =>
            {
                src.UpdatedAt = DateTime.UtcNow;
            });
        }
    }
}
