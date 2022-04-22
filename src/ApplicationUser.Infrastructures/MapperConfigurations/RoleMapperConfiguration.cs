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
    public class RoleMapperConfiguration : Profile
    {
        public RoleMapperConfiguration()
        {
            CreateMap<RoleCreationCommand, Role>().AfterMap((des, src) =>
            {
                src.CreatedAt = DateTime.UtcNow;
                src.Id = Guid.NewGuid();
            });
            CreateMap<RoleUpdateCommand, Role>().AfterMap((des, src) =>
            {
                src.UpdatedAt = DateTime.UtcNow;
            });
        }
    }
}
