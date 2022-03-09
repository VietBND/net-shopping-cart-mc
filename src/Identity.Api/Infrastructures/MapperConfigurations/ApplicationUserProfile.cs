using AutoMapper;
using Identity.Api.Infrastructures.Domain;
using Identity.Api.Infrastructures.Domain.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Api.Infrastructures.MapperConfigurations
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<AuthRegisterIntergrationEvent, ApplicationUser>();
        }
    }
}
