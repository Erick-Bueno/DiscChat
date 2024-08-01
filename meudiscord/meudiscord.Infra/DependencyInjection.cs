using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration){
        DotNetEnv.Env.Load();
        var connectionString = configuration.GetConnectionString("default");
        services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql")));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IGuildRepository, GuildRepository>();
        services.AddScoped<IChannelRepository, ChannelRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGuildService, GuildService>();
        services.AddScoped<IChannelService, ChannelService>();
        services.AddScoped<IMessageService, MessageService>();
        

        services.AddScoped<IConvertUserRegisterDto, ConvertUserRegisterDto>();
        services.AddScoped<IConvertGuildDto, ConvertGuildDto>();
        services.AddScoped<IConvertToken, ConvertToken>();
        services.AddScoped<IConvertChannelDto, ConvertChannelDto>();
        services.AddScoped<IConvertMessage, ConvertMessage>();


        return services;
    }
}