using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;
[Collection("database")]
public class MeuDiscordFactory : WebApplicationFactory<Program>
{
    private readonly DbFixture _dbFixture;

    public MeuDiscordFactory(DbFixture dbFixture)
    {
        _dbFixture = dbFixture;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //analisar

        builder.UseEnvironment("Development");
        builder.ConfigureServices(Services =>
        {
            Services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(_dbFixture.connectionString, ServerVersion.AutoDetect(_dbFixture.connectionString)));

            var serviceProvider = Services.BuildServiceProvider();
            using(var scope = serviceProvider.CreateScope()){
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
              
      
        });
        builder.ConfigureAppConfiguration((context, configuration) =>
        {
            configuration.AddEnvironmentVariables();

            configuration.AddInMemoryCollection(new[]{
                new KeyValuePair<string,string>("ConnectionString:CONNECTION_STRING", _dbFixture.connectionString)
            });
        });
    }
}