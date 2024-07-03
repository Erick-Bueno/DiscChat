using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure (this IServiceCollection services){
        DotNetEnv.Env.Load();
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql")));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IGuildRepository, GuildRepository>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGuildService, GuildService>();
        

        services.AddScoped<IConvertUserRegisterDto, ConvertUserRegisterDto>();
        services.AddScoped<IConvertGuildDto, ConvertGuildDto>();
        services.AddScoped<IConvertToken, ConvertToken>();


        return services;
    }
}