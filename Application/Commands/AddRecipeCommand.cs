using Application.Domain.Interfaces.Repositories;
using Domain.Entities.Aggregates.Recipe;
using MediatR;

namespace Application.Commands
{
    public class AddRecipeCommand : IRequest<bool>
    {
        public string? Name { get; set; }
    }

    public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipRepository;
        public AddRecipeCommandHandler(IRecipeRepository recipRepository) 
        {
            _recipRepository = recipRepository;
        }
        public async Task<bool> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
            Recipe recipe = new(name: request.Name);

            await _recipRepository.AddRecipe(recipe);

            await _recipRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
