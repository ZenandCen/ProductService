using Microsoft.EntityFrameworkCore;
using ProductService.Common.Interfaces;
using System.Text.Json;

namespace ProductService.EventSourcing.PostgresEventStore
{
    public class PostgresEventStoreRepository : IEventStore
    {
        private readonly EventStoreDbContext _dbContext;

        public PostgresEventStoreRepository(EventStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveEventAsync<T>(T @event) where T : class
        {
            var aggregateId = (Guid)@event!.GetType().GetProperty("ProductId")!.GetValue(@event)!;

            var entity = new EventEntity
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregateId,
                EventType = @event.GetType().AssemblyQualifiedName!,
                Data = JsonSerializer.Serialize(@event),
                Timestamp = DateTime.UtcNow
            };

            _dbContext.Events.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetEventsAsync(Guid aggregateId)
        {
            var events = await _dbContext.Events
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.Timestamp)
                .ToListAsync();

            return events
                .Select(e => JsonSerializer.Deserialize(e.Data, Type.GetType(e.EventType)!))
                .Where(e => e != null)!;
        }
    }
}
