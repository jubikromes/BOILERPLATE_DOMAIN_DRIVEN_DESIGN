using SharedKernel.EventBus.Events;

namespace Application.Events
{
    public record IngredientPriceChangedEvent : IntegrationEvent
    {
        public int IngredientId { get; private init; }

        public decimal NewPrice { get; private init; }

        public decimal OldPrice { get; private init; }

        public IngredientPriceChangedEvent(int ingredientId, decimal newPrice, decimal oldPrice)
        {
            IngredientId = ingredientId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
