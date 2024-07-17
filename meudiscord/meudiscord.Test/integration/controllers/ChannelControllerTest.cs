using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

[Collection("database")]
public class ChannelControllerTest : IClassFixture<MeuDiscordFactory>
{
    private readonly MeuDiscordFactory _factory;

    public ChannelControllerTest(MeuDiscordFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void should_return_error_enter_a_name_for_the_channel_when_create_channel()
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
        var request = new ChannelDto
        {
            channelName = "",
            externalIdServer = Guid.NewGuid()
        };
        var response = await client.PostAsJsonAsync("api/Channels", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var createChannelResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var channelNameErrors = createChannelResponse.errors["channelName"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, createChannelResponse.status);
        Assert.Contains("Informe um nome para o canal", channelNameErrors);
    }
    [Fact]
    public async void should_return_badrequest_invalid_server_when_create_channel()
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
        var request = new ChannelDto
        {
            channelName = "teste",
            externalIdServer = Guid.NewGuid()
        };
        var response = await client.PostAsJsonAsync("api/Channels", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var createChannelResponse = JsonConvert.DeserializeObject<InvalidServerError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.IsType<InvalidServerError>(createChannelResponse);
    }
    [Fact]
    public async void should_return_created_when_create_channel()
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
        var request = new ChannelDto
        {
            channelName = "teste",
            externalIdServer = Guid.Parse("2b6829f6-795a-454a-9ab2-13893e415608")
        };
        var response = await client.PostAsJsonAsync("api/Channels", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var createChannelResponse = JsonConvert.DeserializeObject<ResponseCreateChannel>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(201, createChannelResponse.status);
        Assert.Equal("Servidor criado com sucesso", createChannelResponse.message);
    }
    [Fact]
    public async void should_return_badrequest_invalid_server_when_get_all_channels()
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
        var response = await client.GetAsync($"api/Channels/{Guid.NewGuid()}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var createChannelResponse = JsonConvert.DeserializeObject<InvalidServerError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.IsType<InvalidServerError>(createChannelResponse);
    }
    [Fact]
    public async void should_return_Ok_when_get_all_channels()
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
        var response = await client.GetAsync($"api/Channels/{Guid.Parse("2b6829f6-795a-454a-9ab2-13893e415608")}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var createChannelResponse = JsonConvert.DeserializeObject<ResponseAllChannels>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(200, createChannelResponse.status);
        Assert.Equal("Canais encontrados", createChannelResponse.message);
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
            externalId = Guid.Parse("2b6829f6-795a-454a-9ab2-13893e415608")
        };
        db.servers.Add(server);
        db.SaveChanges();
    }
}