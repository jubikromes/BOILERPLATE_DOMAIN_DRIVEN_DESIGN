using API;


var configuration = GetConfiguration();


try
{
    var host = CreateWebHostBuilder(configuration, args);
    IHost builder = host.Build();
    builder.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

finally
{

}

IHostBuilder CreateWebHostBuilder(IConfiguration configuration, string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
        .UseContentRoot(Directory.GetCurrentDirectory());
        //.Build();


IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>(true);

    var config = builder.Build();

    return builder.Build();
}