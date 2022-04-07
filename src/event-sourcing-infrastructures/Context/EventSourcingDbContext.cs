using EventSourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.EfCore;

namespace event_sourcing_infrastructures.Context
{
    public class EventSourcingDbContext : VBDbContext
    {
        private readonly IConfiguration _configuration;
        public EventSourcingDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventStore>().HasKey(s => s.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_configuration.GetConnectionString("EventStore"), ServerVersion.AutoDetect(_configuration.GetConnectionString("EventStore")));
        }

        public virtual DbSet<EventStore> EventStores { get; set; }
    }
}
