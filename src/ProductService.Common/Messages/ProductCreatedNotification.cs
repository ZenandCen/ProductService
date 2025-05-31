namespace ProductService.Common.Messages
{
    public record ProductCreatedNotification
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
