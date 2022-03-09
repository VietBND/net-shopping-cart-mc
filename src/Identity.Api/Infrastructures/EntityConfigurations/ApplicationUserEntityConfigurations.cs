using Identity.Api.Infrastructures.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;

namespace Identity.Api.Infrastructures.EntityConfigurations
{
    public class ApplicationUserEntityConfigurations : BaseEntityTypeConfiguration<ApplicationUser,long>
    {
        public override void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            base.Configure(builder);
        }
    }
}
