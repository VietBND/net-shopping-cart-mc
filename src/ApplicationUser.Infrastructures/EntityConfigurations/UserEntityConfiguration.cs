using ApplicationUser.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;

namespace ApplicationUser.Infrastructures.EntityConfigurations
{
    public class UserEntityConfiguration : BaseAggregateRootTypeConfiguration<User,long>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
            builder.Property(s => s.Email).HasMaxLength(255).IsRequired();
            builder.Property(s => s.UserName).HasMaxLength(100).IsRequired();
            builder.Property(s => s.Password).HasMaxLength(80).IsRequired();
            base.Configure(builder);
        }
    }
}
