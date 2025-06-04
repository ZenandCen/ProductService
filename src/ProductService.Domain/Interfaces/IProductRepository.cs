using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IQueryable<Product>> GetAllProductsAsync(string? search, int page, int pageSize);
        Task AddAsync(Product product);
    }
}
