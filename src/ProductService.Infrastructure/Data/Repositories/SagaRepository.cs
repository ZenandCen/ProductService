using Microsoft.EntityFrameworkCore;
using ProductService.Common.Interfaces;
using ProductService.Common.Sagas;

namespace ProductService.Infrastructure.Data.Repositories
{
    public class SagaRepository : ISagaRepository
    {
        private readonly SagaDbContext _context;

        public SagaRepository(SagaDbContext context)
        {
            _context = context;
        }

        public async Task<ProductCreationSagaState> GetByCorrelationIdAsync(Guid correlationId)
        {
            return await _context.ProductCreationSagaStates
                .FirstOrDefaultAsync(s => s.CorrelationId == correlationId);
        }

        public async Task AddAsync(ProductCreationSagaState sagaState)
        {
            await _context.ProductCreationSagaStates.AddAsync(sagaState);
        }

        public async Task UpdateAsync(ProductCreationSagaState sagaState)
        {
            _context.ProductCreationSagaStates.Update(sagaState);
        }
    }
}
