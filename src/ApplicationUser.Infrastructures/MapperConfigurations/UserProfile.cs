using ApplicationUser.Domain.Commands;
using ApplicationUser.Domain.UserAggregate;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Common;

namespace ApplicationUser.Infrastructures.MapperConfigurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterCommand, User>().AfterMap((s,d) =>
            {
                d.Password = Encryption.HashPassword(s.Password, out string salt);
                d.CreatedAt = DateTime.UtcNow;
                d.CreatedBy = string.IsNullOrEmpty(d.CreatedBy) ? string.Empty : d.CreatedBy;
                d.Salt = salt;
            });
        }
    }
}
