using Evently.Modules.Events.Api.Events;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Api.Database;

public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options)
{
    internal DbSet<Event>  Events { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);

        modelBuilder.Entity<Event>()
            .Property(x => x.Title).HasMaxLength(256);

        modelBuilder.Entity<Event>()
            .Property(x => x.Description).HasMaxLength(2048);
        
        modelBuilder.Entity<Event>()
            .Property(x => x.Location).HasMaxLength(1024);
    }
}
