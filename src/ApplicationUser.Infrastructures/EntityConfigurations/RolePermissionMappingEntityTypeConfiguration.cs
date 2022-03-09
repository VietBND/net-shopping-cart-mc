using ApplicationUser.Domain.RoleAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;

namespace ApplicationUser.Infrastructures.EntityConfigurations
{
    public class RolePermissionMappingEntityTypeConfiguration : BaseEntityTypeConfiguration<RolePermissionMapping,Guid>
    {
        public override void Configure(EntityTypeBuilder<RolePermissionMapping> builder)
        {
            builder.Ignore(s => s.Id);
            builder.HasOne(s => s.Permission).WithMany(s => s.RolePermissionMappings).HasForeignKey(s => s.PermissionId);
            builder.HasOne(s => s.Role).WithMany(s => s.RolePermissionMappings).HasForeignKey(s => s.RoleId);
            builder.HasKey(s => new { s.PermissionId, s.RoleId });
        }
    }
}
