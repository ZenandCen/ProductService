using MassTransit;

namespace ProductService.Common.Sagas
{
    public class ProductCreationSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
