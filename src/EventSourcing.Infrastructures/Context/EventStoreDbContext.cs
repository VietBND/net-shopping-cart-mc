using EventSourcing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.EfCore;

namespace EventSourcing.Infrastructures.Context
{
    public class EventStoreDbContext : VBDbContext
    {
        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventLog>().HasKey(s => s.Id);
        }

        public virtual DbSet<EventLog> EventLogs { get; set; }
    }
}
