using Microsoft.EntityFrameworkCore;

namespace ProductService.EventSourcing.PostgresEventStore
{
    public class EventStoreDbContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; }

        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("event_store");
            modelBuilder.Entity<EventEntity>().ToTable("EventStore");
        }
    }
}
