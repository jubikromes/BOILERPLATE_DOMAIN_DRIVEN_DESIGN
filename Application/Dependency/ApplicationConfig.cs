using Application.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Dependency
{
    public static class ApplicationConfig
    {
        public static void ConfigureApplicaion(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(AddRecipeCommand)));
        }
    }
}
