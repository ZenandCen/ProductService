namespace ProductService.EventSourcing.PostgresEventStore
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string EventType { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
