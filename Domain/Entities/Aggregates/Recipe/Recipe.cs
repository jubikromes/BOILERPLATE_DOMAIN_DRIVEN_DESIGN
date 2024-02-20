using SharedKernel;
using SharedKernel.Interfaces;

namespace Domain.Entities.Aggregates.Recipe
{
    public class Recipe : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public Recipe(string name)
        {
            Name = name;
        }
    }
}
