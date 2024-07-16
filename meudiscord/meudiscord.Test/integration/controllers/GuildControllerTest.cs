using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

[Collection("database")]
public class GuildControllerTest : IClassFixture<MeuDiscordFactory>
{
    private readonly MeuDiscordFactory _factory;

    public GuildControllerTest(MeuDiscordFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void should_return_badrequest_no_guilds_found_when_get_all_guilds()
    {
        string jwt;
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var configuration = scopedServices.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var jwtService = new JwtService(configuration);
            var user = new UserModel("erick", "erickjb93@gmail.com", "sirlei231@")
            {
                id = 1
            };
            jwt = jwtService.GenerateAccessToken(user);
        }

        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var response = await client.GetAsync("api/Guilds");
        var responseContent = await response.Content.ReadAsStringAsync();
        var getAllGuildsResponse = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal(404, getAllGuildsResponse.status);
        Assert.Equal("Nenhum servidor foi encontrado", getAllGuildsResponse.message);
    }
    [Fact]
    public async void should_return_ok_when_get_all_guilds()
    {
        string jwt;
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var configuration = scopedServices.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeDbForTests(db);
            var jwtService = new JwtService(configuration);
            var user = new UserModel("erick", "erickjb93@gmail.com", "sirlei231@")
            {
                id = 1
            };
            jwt = jwtService.GenerateAccessToken(user);
        }

        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var response = await client.GetAsync("api/Guilds");
        var responseContent = await response.Content.ReadAsStringAsync();
        var getAllGuildsResponse = JsonConvert.DeserializeObject<ResponseAllGuilds>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(200, getAllGuildsResponse.status);
        Assert.Equal("Guildas encontradas", getAllGuildsResponse.message);

    }
    [Fact]
    public async void should_return_badrequest_unable_to_create_the_server_when_create_guild()
    {
        string jwt;
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var configuration = scopedServices.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var jwtService = new JwtService(configuration);
            var user = new UserModel("erick", "erickjb93@gmail.com", "sirlei231@")
            {
                id = 1
            };
            jwt = jwtService.GenerateAccessToken(user);
        }
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var request = new GuildDto
        {
            externalIdUser = Guid.NewGuid(),
            serverName = "teste"
        };
        var response = await client.PostAsJsonAsync("api/Guilds", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var createGuildResponse = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("Não foi possivel criar o servidor", createGuildResponse.message);
        Assert.Equal(404, createGuildResponse.status);
    }
    [Fact]
    public async void should_return_error_server_name_when_create_guild()
    {
        string jwt;
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var configuration = scopedServices.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var jwtService = new JwtService(configuration);
            var user = new UserModel("erick", "erickjb93@gmail.com", "sirlei231@")
            {
                id = 1
            };
            jwt = jwtService.GenerateAccessToken(user);
        }
        var client = _factory.CreateClient();
        var request = new GuildDto
        {
            externalIdUser = Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94"),
            serverName = ""
        };
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var response = await client.PostAsJsonAsync("api/Guilds", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var createGuildResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var serverNameErrors = createGuildResponse.errors["serverName"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("Informe um nome ao servidor", serverNameErrors);
        Assert.Equal(400, createGuildResponse.status);
    }
    [Fact]
    public async void should_return_ok_when_create_guild()
    {
        string jwt;
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var configuration = scopedServices.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeDbForTests(db);
            var jwtService = new JwtService(configuration);
            var user = new UserModel("erick", "erickjb93@gmail.com", "sirlei231@")
            {
                id = 1
            };
            jwt = jwtService.GenerateAccessToken(user);
        }
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var request = new GuildDto
        {
            externalIdUser = Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94"),
            serverName = "teste"
        };
        var response = await client.PostAsJsonAsync("api/Guilds", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var createGuildResponse = JsonConvert.DeserializeObject<ResponseCreateGuild>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(201, createGuildResponse.status);
        Assert.Equal("Servidor criado com sucesso", createGuildResponse.message);
    }
    [Fact]
    public async void should_return_badrequest_unable_to_delete_the_guild_when_delete_guild()
    {
        string jwt;
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var configuration = scopedServices.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeDbForTests(db);
            var jwtService = new JwtService(configuration);
            var user = new UserModel("erick", "erickjb93@gmail.com", "sirlei231@")
            {
                id = 1
            };
            jwt = jwtService.GenerateAccessToken(user);
        }
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var response = await client.DeleteAsync($"api/Guilds/{Guid.NewGuid()}/{Guid.NewGuid()}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var deleteGuildResponse = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("Não foi possivel deletar o servidor", deleteGuildResponse.message);
        Assert.Equal(404, deleteGuildResponse.status);
    }
    [Fact]
    public async void should_return_badrequest_server_does_not_belong_to_the_user_when_delete_guild()
    {
        string jwt;
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var configuration = scopedServices.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeDbForTests(db);
            var jwtService = new JwtService(configuration);
            var user = new UserModel("erick", "erickjb93@gmail.com", "sirlei231@")
            {
                id = 1
            };
            jwt = jwtService.GenerateAccessToken(user);
        }
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var response = await client.DeleteAsync($"api/Guilds/{Guid.NewGuid()}/{Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94")}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var deleteGuildResponse = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(400, deleteGuildResponse.status);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("O servidor não pertence ao usuário que esta tentando deleta-lo", deleteGuildResponse.message);
    }
    [Fact]
    public async void should_return_ok_when_delete_guild()
    {
        string jwt;
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var configuration = scopedServices.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeDbForTests(db);
            var jwtService = new JwtService(configuration);
            var user = new UserModel("erick", "erickjb93@gmail.com", "sirlei231@")
            {
                id = 1
            };
            jwt = jwtService.GenerateAccessToken(user);
        }
        var client = _factory.CreateClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var response = await client.DeleteAsync($"api/Guilds/{Guid.Parse("3c487866-45f3-4f90-8fb9-7e2f308cf9b0")}/{Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94")}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var deleteGuildResponse = JsonConvert.DeserializeObject<ResponseSuccessDefault>(responseContent);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Servidor deletado com sucesso", deleteGuildResponse.message);
        Assert.Equal(200, deleteGuildResponse.status);

    }
    private void InitializeDbForTests(AppDbContext db)
    {
        var user = new UserModel("erick", "erickjb93@gmail.com", "$2a$12$TyTP3Zj.VQsDgPbf9h7Tvu7bMR1J8fXDnBo7pXxns0Sz0/3E15VMe")
        {
            externalId = Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94")
        };
        db.users.Add(user);
        db.SaveChanges();

        var server = new ServerModel("teste22", user.id)
        {
            externalId = Guid.Parse("3c487866-45f3-4f90-8fb9-7e2f308cf9b0")
        };
        db.servers.Add(server);
        db.SaveChanges();
    }
}