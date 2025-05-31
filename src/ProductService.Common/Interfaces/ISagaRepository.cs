using ProductService.Common.Sagas;

namespace ProductService.Common.Interfaces
{
    public interface ISagaRepository
    {
        Task<ProductCreationSagaState> GetByCorrelationIdAsync(Guid correlationId);
        Task AddAsync(ProductCreationSagaState sagaState);
        Task UpdateAsync(ProductCreationSagaState sagaState);
    }
}
