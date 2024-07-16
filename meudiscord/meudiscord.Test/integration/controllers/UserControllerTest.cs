using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

[Collection("database")]
public class UserControllerTest : IClassFixture<MeuDiscordFactory>
{
    private readonly MeuDiscordFactory _factory;

    public UserControllerTest(MeuDiscordFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void should_return_badrequest_user_not_found()
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
        var response = await client.GetAsync($"api/User/{Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94")}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var createUserResponse = JsonConvert.DeserializeObject<ResponseError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("Usuário não encontrado", createUserResponse.message);
        Assert.Equal(404, createUserResponse.status);
    }
    [Fact]
    public async void should_return_ok_when_find_user_authenticated()
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
        var response = await client.GetAsync($"api/User/{Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94")}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var createUserResponse = JsonConvert.DeserializeObject<ResponseUserData>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(200, createUserResponse.status);
        Assert.Equal("Usuário encontrado", createUserResponse.message);

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