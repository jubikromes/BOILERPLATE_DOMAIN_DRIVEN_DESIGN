using SharedKernel.EventBus.Events;

namespace SharedKernel.EventBus.Interfaces.EventHandlers;
public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}

public interface IIntegrationEventHandler
{
}
