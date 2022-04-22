using ApplicationUser.Domain.Entities;
using ApplicationUser.Infrastructures.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.EfCore;

namespace ApplicationUser.Infrastructures.Context
{
    public class ApplicationUserDbContext : VBDbContext
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine(environment);
            var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.Development.json").Build();
            SeedHelper.GeneratePermissionsDataWithCsvFile(configuration.GetConnectionString("Default")).Wait();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }
    }
}
