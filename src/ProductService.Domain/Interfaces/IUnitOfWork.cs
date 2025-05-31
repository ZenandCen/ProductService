using Microsoft.EntityFrameworkCore.Storage;
using ProductService.Common.Interfaces;

namespace ProductService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISagaRepository SagaRepository { get; }
        IProductRepository ProductRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }

}
