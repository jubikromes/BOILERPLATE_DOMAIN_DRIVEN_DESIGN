using Domain.Entities.Aggregates.Recipe;
using SharedKernel.Interfaces;

namespace Application.Domain.Interfaces.Repositories;
 public interface IRecipeRepository : IRepository<Recipe>
{
    Task<Recipe> GetRecipeById(Guid id);
    Task<bool> AddRecipe(Recipe recipe);
}
