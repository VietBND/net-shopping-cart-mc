using ApplicationUser.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;

namespace ApplicationUser.Infrastructures.EntityConfigurations
{
    public class UserRoleMappingEntityTypeConfiguration : BaseEntityTypeConfiguration<UserRoleMapping, Guid>
    {
        public override void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.Ignore(s => s.Id);
            builder.HasOne(s => s.User).WithMany(s => s.UserRoleMappings).HasForeignKey(s => s.UserId);
            builder.HasOne(s => s.Role).WithMany(s => s.UserRoleMappings).HasForeignKey(s => s.RoleId);
            builder.HasKey(s => new { s.UserId, s.RoleId });
        }
    }
}
