using ApplicationUser.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;

namespace ApplicationUser.Infrastructures.EntityConfigurations
{
    public class TenantEntityTypeConfiguration : BaseEntityTypeConfiguration<Tenant,int>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            base.Configure(builder);
        }
    }
}
