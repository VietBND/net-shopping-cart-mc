using Identity.Api.Infrastructures.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;

namespace Identity.Api.Infrastructures
{
    public class IdentityDbContext : DbContext, IDbContext<IdentityDbContext>
    {
        public IdentityDbContext Instance => this;
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Identity.Api"));
        }

        public virtual DbSet<Infrastructures.Domain.ApplicationUser> ApplicationUsers { get; set; }
    }
}
