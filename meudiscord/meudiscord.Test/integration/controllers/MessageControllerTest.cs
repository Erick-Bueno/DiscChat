using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

[Collection("database")]
public class MessageControllerTest : IClassFixture<MeuDiscordFactory>
{
    private readonly MeuDiscordFactory _factory;

    public MessageControllerTest(MeuDiscordFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void should_return_badrequest_invalid_channel_when_get_old_messages()
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
        var response = await client.GetAsync($"api/Messages/{Guid.NewGuid()}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var messageResponse = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, messageResponse.status);
        Assert.Equal("Canal invalido", messageResponse.message);
    }

    [Fact]
    public async void should_return_ok_when_get_old_messages()
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
        var response = await client.GetAsync($"api/Messages/{Guid.Parse("97a4bb9c-c0f4-4181-8992-e1c6a29ddb66")}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var messageResponse = JsonConvert.DeserializeObject<ResponseGetOldMessages>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(200, messageResponse.status);
        Assert.Equal("Mensagens encontradas", messageResponse.message);
    }
    [Fact]
    public async void should_return_badrequest_channel_not_found_when_delete_message_in_channel()
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
        var response = await client.DeleteAsync($"api/Messages/{Guid.NewGuid()}/{Guid.NewGuid()}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDeleteMessage = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal(404, responseDeleteMessage.status);
        Assert.Equal("Canal não encontrado", responseDeleteMessage.message);

    }
    [Fact]
    public async void should_return_bad_request_message_not_found_when_delete_message_in_channel()
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
        var response = await client.DeleteAsync($"api/Messages/{Guid.Parse("97a4bb9c-c0f4-4181-8992-e1c6a29ddb66")}/{Guid.NewGuid()}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDeleteMessage = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal(404, responseDeleteMessage.status);
        Assert.Equal("Mensagem não encontrada", responseDeleteMessage.message);

    }
    [Fact]
    public async void should_return_ok_when_delete_message_in_channel()
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
        var response = await client.DeleteAsync($"api/Messages/{Guid.Parse("97a4bb9c-c0f4-4181-8992-e1c6a29ddb66")}/{Guid.Parse("ee89105d-4b6a-4141-b085-3d544b35fd98")}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDeleteMessage = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(200, responseDeleteMessage.status);
        Assert.Equal("Mensagem deletada com sucesso", responseDeleteMessage.message);
    }
    private void InitializeDbForTests(AppDbContext db)
    {
        var user = new UserModel("erick", "erickjb93@gmail.com", "$2a$12$TyTP3Zj.VQsDgPbf9h7Tvu7bMR1J8fXDnBo7pXxns0Sz0/3E15VMe")
        {
            externalId = Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94"),

        };
        db.users.Add(user);
        db.SaveChanges();

        var server = new ServerModel("teste22", user.id)
        {
            externalId = Guid.Parse("3c487866-45f3-4f90-8fb9-7e2f308cf9b0")
        };
        db.servers.Add(server);
        db.SaveChanges();

        var channel = new ChannelModel()
        {
            id = 1,
            externalId = Guid.Parse("97a4bb9c-c0f4-4181-8992-e1c6a29ddb66"),
            name = "teste",
            serverId = server.id,

        };
        db.channels.Add(channel);
        db.SaveChanges();
        var message = new MessageModel()
        {
            externalId = Guid.Parse("ee89105d-4b6a-4141-b085-3d544b35fd98"),
            idChannel = channel.id,
            idUser = user.id,
            message = "teste"
        };
        db.messages.Add(message);
        db.SaveChanges();
        var message2 = new MessageModel()
        {
            idChannel = channel.id,
            idUser = user.id,
            message = "bosta"
        };
        db.messages.Add(message2);
        db.SaveChanges();
    }
}