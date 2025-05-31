using MassTransit;
using Microsoft.Extensions.Logging;
using ProductService.Common.Messages;

namespace ProductService.BackgroundServices.Consumers
{
    public class NotificationConsumer : IConsumer<ProductCreatedNotification>
    {
        private readonly ILogger<NotificationConsumer> _logger;

        public NotificationConsumer(ILogger<NotificationConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductCreatedNotification> context)
        {
            var message = context.Message;
            _logger.LogInformation($"Product Created Notification: ProductId={message.ProductId}, Name={message.Name}, Timestamp={message.Timestamp}");

            // Thêm logic gửi thông báo (ví dụ: gửi email, gọi API bên ngoài)
            // Ví dụ: Gửi email
            // await SendEmailAsync(message);

            return Task.CompletedTask;
        }

        // Ví dụ: Hàm gửi email (nếu cần)
        private async Task SendEmailAsync(ProductCreatedNotification notification)
        {
            // Logic gửi email (có thể dùng SMTP hoặc dịch vụ như SendGrid)
            await Task.CompletedTask;
            _logger.LogInformation($"Email sent for product: {notification.Name}");
        }
    }
}
