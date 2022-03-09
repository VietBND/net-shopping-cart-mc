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
    public class PermissionEntityTypeConfiguration : BaseEntityTypeConfiguration<Permission,Guid>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
            base.Configure(builder);
        }
    }
}
