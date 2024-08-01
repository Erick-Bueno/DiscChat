using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly IConfiguration configuration;

    public AppDbContextFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString = configuration.GetConnectionString("default");
        var options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"), m => m.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        return new AppDbContext(options.Options);
    }
}