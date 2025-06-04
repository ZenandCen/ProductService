
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Product>> GetAllProductsAsync(string? search, int page, int pageSize)
        {
            return _context.Products.Where(x => x.Name.Contains(search ?? string.Empty))
                                               .OrderBy(x => x.Name)
                                               .Skip((page - 1) * pageSize)
                                               .Take(pageSize);
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }
    }
}
