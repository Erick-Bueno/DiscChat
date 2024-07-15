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

        builder.UseEnvironment("Development");
        builder.ConfigureServices(Services =>
        {
            Services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(_dbFixture.connectionString, ServerVersion.AutoDetect(_dbFixture.connectionString)));

         
      
        });
        builder.ConfigureAppConfiguration((context, configuration) =>
        {
            configuration.AddEnvironmentVariables();

            configuration.AddInMemoryCollection(new[]{
                new KeyValuePair<string,string>("ConnectionStrings:CONNECTION_STRING", _dbFixture.connectionString)
            });
        });
    }
}