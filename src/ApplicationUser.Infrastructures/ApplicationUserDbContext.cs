using ApplicationUser.Domain.Entities;
using ApplicationUser.Domain.RoleAggregate;
using ApplicationUser.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VietBND.DbContext;

namespace ApplicationUser.Infrastructures
{
    public class ApplicationUserDbContext : DbContext, IDbContext<ApplicationUserDbContext>
    {
        public ApplicationUserDbContext Instance => this;
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("ApplicationUser.Infrastructures"));
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public virtual DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
    }
}
