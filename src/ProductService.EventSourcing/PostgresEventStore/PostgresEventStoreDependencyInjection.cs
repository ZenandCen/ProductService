using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductService.EventSourcing.PostgresEventStore
{
    public static class PostgresEventStoreDependencyInjection
    {
        public static IServiceCollection AddPostgresEventStore(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EventStoreDbContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }
    }
}
