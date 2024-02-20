using API.Filters;
using Application.Dependency;
using Application.EventHandlers;
using Application.Events;
using Infrastructure.Dependency;
using SharedKernel.Dependency;
using SharedKernel.EventBus.Interfaces;

namespace API;
 public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureDbContext(Configuration);
        services.ConfigureApplicaion();
        services.ConfigureInfrastructure();
        services.ConfigureSwagger(Configuration);

        services.ConfigureEventBus(Configuration);
        services.AddControllers(opt =>
        {
            opt.Filters.Add(typeof(HttpGlobalExceptionFilter));
        });

        services.AddTransient<IngredientPriceChangedEventHandler>();
    }

    private void ConfigureEventBus(IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

        eventBus.Subscribe<IngredientPriceChangedEvent, IngredientPriceChangedEventHandler>();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint($"/swagger/v1/swagger.json", "RecipeManagement.API V1");
                  c.OAuthClientId("RecipeManagement");
                  c.OAuthAppName("Recipe Management Swagger UI");
              });
        app.UseRouting();

        //app.UseHttpsRedirection();

        app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "RecipeManagement V1"); });

        //app.UseStaticFiles();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();

            endpoints.MapControllers();
        });

        ConfigureEventBus(app);
    }
}
