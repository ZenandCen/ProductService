using Microsoft.Extensions.DependencyInjection;
using ProductService.BackgroundServices.Services;

namespace ProductService.BackgroundServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<NotificationService>();
            return services;
        }
    }
}
