using Application.Domain.Interfaces.Repositories;
using Domain.Entities.Aggregates.Recipe;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dependency
{
    public static class InfrastructureConfig
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IRecipeRepository, RecipeRepository>();

        }
    }
}
