namespace ProductService.Domain.DomainEvents
{
    public class ProductCreatedEvent
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime OccurredOn { get; set; }

        public ProductCreatedEvent(Guid productId, string name, decimal price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
