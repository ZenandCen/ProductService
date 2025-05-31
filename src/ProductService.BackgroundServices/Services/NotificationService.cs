using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductService.Common.Messages;

namespace ProductService.BackgroundServices.Services
{
    public class NotificationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<NotificationService> _logger;


        public NotificationService(IServiceScopeFactory scopeFactory, ILogger<NotificationService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Notification Service is running...");
            while (!stoppingToken.IsCancellationRequested)
            {
                // Wait before the next execution (e.g., every 10 seconds)
                await Task.Delay(10000, stoppingToken);
            }
        }

        public async Task PublishProductCreatedNotification(Guid productId, string name, CancellationToken cancellationToken = default)
        {
            try
            {
                // Create a scope for resolving scoped services
                using (var scope = _scopeFactory.CreateScope())
                {
                    // Resolve IPublishEndpoint within the scope
                    var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                    // Publish the ProductCreatedNotification
                    await publishEndpoint.Publish(new ProductCreatedNotification
                    {
                        ProductId = productId,
                        Name = name,
                        Timestamp = DateTime.UtcNow
                    }, cancellationToken);
                }

                _logger.LogInformation("Published ProductCreatedNotification for ProductId: {ProductId}, Name: {Name}", productId, name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing ProductCreatedNotification for ProductId: {ProductId}", productId);
                throw; // Optionally rethrow or handle as needed
            }
        }
    }
}
