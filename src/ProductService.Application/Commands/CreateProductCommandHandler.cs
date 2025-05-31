using MassTransit;
using MediatR;
using ProductService.Common.Interfaces;
using ProductService.Domain.DomainEvents;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IEventStore eventStore, IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
            _publishEndpoint = publishEndpoint;

        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Price);
            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //lưu ProductCreatedEvent vào EventStoreDb
            var productCreatedEvent = new ProductCreatedEvent(product.Id, product.Name, product.Price);
            await _eventStore.SaveEventAsync(productCreatedEvent);

            // Phát sự kiện qua RabbitMQ
            await _publishEndpoint.Publish(productCreatedEvent, cancellationToken);

            return product.Id;
        }
    }
}
