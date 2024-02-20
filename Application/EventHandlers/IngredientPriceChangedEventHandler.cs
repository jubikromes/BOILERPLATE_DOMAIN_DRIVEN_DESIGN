using Application.Events;
using SharedKernel.EventBus.Interfaces.EventHandlers;

namespace Application.EventHandlers;
 public class IngredientPriceChangedEventHandler : IIntegrationEventHandler<IngredientPriceChangedEvent>
{
    public IngredientPriceChangedEventHandler()
    {

    }
    public Task Handle(IngredientPriceChangedEvent @event)
    {
        throw new NotImplementedException();
    }
}