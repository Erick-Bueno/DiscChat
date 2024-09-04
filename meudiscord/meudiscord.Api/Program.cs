using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
public class Program
{
    public static void Main(string[] args)
    {
        DotNetEnv.Env.Load("../.env");
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors();
        builder.Services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo{
                Title = "MeuDiscord.Api",
                Version = "v1",
                Contact = new OpenApiContact{
                    Name = "Erick Jhonata",
                    Email = "erickjb93@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/erick-jhonata-35544320a/")
                }
            });
            var xmlFile = "meudiscord.Api.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
                Description = "Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddSignalR();
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("KeyAccessToken"))),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });

        var app = builder.Build();
        app.UseCors(c => {
            c.AllowCredentials();
            c.AllowAnyHeader();
            c.AllowAnyMethod();
            c.WithOrigins("http://localhost:4200");
        
        });
        app.MapHub<ChatHub>("/chat");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

