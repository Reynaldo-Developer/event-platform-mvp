using EventService.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EventService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EventEntity> Events => Set<EventEntity>();
        public DbSet<ZoneEntity> Zones => Set<ZoneEntity>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
