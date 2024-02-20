using Application.Domain.Interfaces.Repositories;
using Domain.Entities.Aggregates.Recipe;
using Infrastructure.Context;
using SharedKernel.Interfaces;

namespace Infrastructure.Repositories;
public class RecipeRepository : IRecipeRepository
{
    private readonly DDDContext _context;


    public IUnitOfWork UnitOfWork => _context;

    public RecipeRepository(DDDContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Recipe> GetRecipeById(Guid id) => await _context.Recipes.FindAsync(id);

    public async Task<bool> AddRecipe(Recipe recipe)
    {
        await _context.AddAsync(recipe);

        return true;
    }
}
