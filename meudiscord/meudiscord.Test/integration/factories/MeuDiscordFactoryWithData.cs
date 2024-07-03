using DevOne.Security.Cryptography.BCrypt;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;
[Collection("database")]
public class MeuDiscordFactoryWithData : WebApplicationFactory<Program>
{
    private readonly DbFixture _dbFixture;

    public MeuDiscordFactoryWithData(DbFixture dbFixture)
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
    
            
            var serviceProvider = Services.BuildServiceProvider();
            using(var scope = serviceProvider.CreateScope()){
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                InitializeDbForTests(db);
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
    private void InitializeDbForTests(AppDbContext db){
        var user = new UserModel("erick","erickjb93@gmail.com","$2a$12$TyTP3Zj.VQsDgPbf9h7Tvu7bMR1J8fXDnBo7pXxns0Sz0/3E15VMe"){
            externalId = Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94")
        };
        db.users.Add(user);
        db.SaveChanges();
        
        var server = new ServerModel("teste22", user.id);
        db.servers.Add(server);
        db.SaveChanges();
    }
}