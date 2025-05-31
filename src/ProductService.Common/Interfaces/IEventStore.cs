namespace ProductService.Common.Interfaces
{
    public interface IEventStore
    {
        Task SaveEventAsync<T>(T @event) where T : class;
        Task<IEnumerable<object>> GetEventsAsync(Guid aggregateId);
    }
}
