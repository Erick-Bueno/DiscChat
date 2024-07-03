using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        DotNetEnv.Env.Load("../.env");
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        var options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"), m => m.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        return new AppDbContext(options.Options);
    }
}