using MassTransit;
using ProductService.Common.Messages;
using ProductService.Common.Sagas;
using ProductService.Domain.DomainEvents;

namespace ProductService.Application.Sagas
{
    public class ProductCreationSaga : MassTransitStateMachine<ProductCreationSagaState>
    {
        public State ProductCreated { get; private set; }
        public State NotificationSent { get; private set; }

        public Event<ProductCreatedEvent> ProductCreatedEvent { get; private set; }

        public ProductCreationSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => ProductCreatedEvent, x => x.CorrelateById(context => context.Message.ProductId));

            Initially(
                When(ProductCreatedEvent)
                    .Then(context =>
                    {
                        context.Saga.ProductId = context.Message.ProductId;
                        context.Saga.ProductName = context.Message.Name;
                    })
                    .TransitionTo(ProductCreated)
                    .Publish(context => new ProductCreatedNotification
                    {
                        ProductId = context.Saga.ProductId,
                        Name = context.Saga.ProductName,
                        Timestamp = DateTime.UtcNow
                    })
                    .TransitionTo(NotificationSent));
        }
    }
}
