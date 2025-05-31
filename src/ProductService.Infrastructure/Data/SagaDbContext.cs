using Microsoft.EntityFrameworkCore;
using ProductService.Common.Sagas;

namespace ProductService.Infrastructure.Data
{
    public class SagaDbContext : DbContext
    {
        public DbSet<ProductCreationSagaState> ProductCreationSagaStates { get; set; }

        public SagaDbContext(DbContextOptions<SagaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình thủ công bảng ProductCreationSagaState
            modelBuilder.Entity<ProductCreationSagaState>(entity =>
            {
                entity.ToTable("ProductCreationSagaState", schema: "saga_store");
                entity.HasKey(e => e.CorrelationId);
                entity.Property(e => e.CorrelationId).IsRequired();
                entity.Property(e => e.CurrentState).IsRequired();
                entity.Property(e => e.ProductId);
                entity.Property(e => e.ProductName);
            });
        }
    }
}
