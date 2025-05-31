using ProductService.Domain.Exceptions;

namespace ProductService.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Product name cannot be empty.");
            if (price < 0) throw new DomainException("Price cannot be negative.");

            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }

        // Phương thức để phát ra Domain Event (nếu cần)
        public void RaiseProductCreatedEvent()
        {
            // Logic phát sự kiện (sẽ tích hợp với Event Sourcing sau)
        }
    }
}
