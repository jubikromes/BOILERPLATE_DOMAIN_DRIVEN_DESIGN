using Application.Domain.Interfaces.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Dependency
{
    public static class DatabaseConfig
    {
        public static void ConfigureDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DDDContext");


            connectionString = configuration.GetConnectionString("DDDContext");

            service.AddDbContext<DDDContext>(options =>
            {
                options.UseSqlServer(connectionString, x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });
        }
    }
}