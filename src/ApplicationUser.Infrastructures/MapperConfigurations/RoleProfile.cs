using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.RoleAggregate;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUser.Infrastructures.MapperConfigurations
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleCreationCommand, Role>().AfterMap((s, d) =>
            {
                d.CreatedAt = DateTime.UtcNow;
                d.CreatedBy = string.IsNullOrEmpty(d.CreatedBy) ? string.Empty : d.CreatedBy;
            });
        }
    }
}
