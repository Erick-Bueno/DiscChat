using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Xunit;
[Collection("database")]
public class AuthControllerTest : IClassFixture<MeuDiscordFactory>
{
    private readonly MeuDiscordFactory _factory;
    public AuthControllerTest(MeuDiscordFactory factory)
    {
        _factory = factory;
    }
    [Fact]
    public async void should_return_badrequest_invalid_email_when_user_login()
    {
        var client = _factory.CreateClient();
        var request = new UserLoginDto
        {
            email = "erickjb93",
            password = "Sirlei231@"
        };
        var response = await client.PostAsJsonAsync("api/Auth/login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var emailErrors = loginResponse.errors["email"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, loginResponse.status);
        Assert.Contains("Informe um email valido", emailErrors);
    }
    [Fact]
    public async void should_return_badrequest_email_is_required_when_user_login()
    {
        var client = _factory.CreateClient();
        var request = new UserLoginDto
        {
            email = "",
            password = "Sirlei231@"
        };
        var response = await client.PostAsJsonAsync("api/Auth/login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var emailErrors = loginResponse.errors["email"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, loginResponse.status);
        Assert.Contains("Informe um email", emailErrors);
    }
    [Fact]
    public async void should_return_badrequest_invalid_password_when_user_login()
    {
        var client = _factory.CreateClient();
        var request = new UserLoginDto
        {
            email = "erickjb93@gmail.com",
            password = "123"
        };
        var response = await client.PostAsJsonAsync("api/Auth/login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var passwordErrors = loginResponse.errors["password"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, loginResponse.status);
        Assert.Contains("A senha deve conter no minimo 8 caracteres, letra minúscula, letra maiúscula, numero e caractere especial", passwordErrors);
    }
    [Fact]
    public async void should_return_badrequest_password_is_required_when_user_login()
    {
        var client = _factory.CreateClient();
        var request = new UserLoginDto
        {
            email = "erickjb93@gmail.com",
            password = ""
        };
        var response = await client.PostAsJsonAsync("api/Auth/login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var passwordErrors = loginResponse.errors["password"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, loginResponse.status);
        Assert.Contains("Informe uma senha", passwordErrors);
    }
    [Fact]

    public async void should_return_badrequest_user_not_registred_when_user_login()
    {

        var client = _factory.CreateClient();
        var request = new UserLoginDto
        {
            email = "erickjb93@gmail.com",
            password = "Sirlei231@"
        };
        var response = await client.PostAsJsonAsync("api/Auth/login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<UserNotRegisteredError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.IsType<UserNotRegisteredError>(loginResponse);


    }
    [Fact]
    public async void should_return_badrequest_password_incorrect_when_user_login()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeDbForTests(db);
        }
        var client = _factory.CreateClient();
        var request = new UserLoginDto
        {
            email = "erickjb93@gmail.com",
            password = "Erick231@"
        };
        var response = await client.PostAsJsonAsync("api/Auth/login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<IncorrectPasswordError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.IsType<IncorrectPasswordError>(loginResponse);
    }
    [Fact]
    public async void should_return_ok_when_user_login()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeDbForTests(db);
        }
        var client = _factory.CreateClient();
        var request = new UserLoginDto
        {
            email = "erickjb93@gmail.com",
            password = "Sirlei231@"
        };
        var response = await client.PostAsJsonAsync("api/Auth/login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<ResponseAuth>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(200, loginResponse.status);
        Assert.Equal("úsuario logado com sucesso", loginResponse.message);
    }
    [Fact]
    public async void should_return_badrequest_name_is_required_when_user_register()
    {
        var client = _factory.CreateClient();
        var request = new UserRegisterDto("", "erickjb93@gmail.com", "Sirlei231@");
        var response = await client.PostAsJsonAsync("api/Auth/register", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var registerResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var nameErrors = registerResponse.errors["name"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, registerResponse.status);
        Assert.Contains("Informe um nome", nameErrors);
    }
    [Fact]
    public async void should_return_badrequest_invalid_email_when_user_register()
    {
        var client = _factory.CreateClient();
        var request = new UserRegisterDto("erick", "erickjb93", "Sirlei231@");
        var response = await client.PostAsJsonAsync("api/Auth/register", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var registerResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var emailErrors = registerResponse.errors["email"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, registerResponse.status);
        Assert.Contains("Informe um email valido", emailErrors);
    }
    [Fact]
    public async void should_return_badrequest_email_is_required_when_user_register()
    {
        var client = _factory.CreateClient();
        var request = new UserRegisterDto("erick", "", "Sirlei231@");
        var response = await client.PostAsJsonAsync("api/Auth/register", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var registerResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var emailErrors = registerResponse.errors["email"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, registerResponse.status);
        Assert.Contains("Informe um email", emailErrors);
    }
    [Fact]
    public async void should_return_badrequest_invalid_password_when_user_register()
    {
        var client = _factory.CreateClient();
        var request = new UserRegisterDto("erick", "erickjb93@gmail.com", "sirlei231");
        var response = await client.PostAsJsonAsync("api/Auth/register", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var registerResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var passwordErrors = registerResponse.errors["password"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, registerResponse.status);
        Assert.Contains("A senha deve conter no minimo 8 caracteres, letra minúscula, letra maiúscula, numero e caractere especial", passwordErrors);
    }
    [Fact]
    public async void should_return_badrequest_password_is_required_when_user_register()
    {
        var client = _factory.CreateClient();
        var request = new UserRegisterDto("erick", "erickjb93@gmail.com", "");
        var response = await client.PostAsJsonAsync("api/Auth/register", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var registerResponse = JsonConvert.DeserializeObject<ResponseDataAnnotationError>(responseContent);
        var passwordErrors = registerResponse.errors["password"];
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(400, registerResponse.status);
        Assert.Contains("Informe uma senha", passwordErrors);
    }
    [Fact]
    public async void should_return_badrequest_email_already_registred_when_user_register()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeDbForTests(db);
        }
        var client = _factory.CreateClient();
        var request = new UserRegisterDto("erick", "erickjb93@gmail.com", "Sirlei231@");
        var response = await client.PostAsJsonAsync("api/Auth/register", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var registerResponse = JsonConvert.DeserializeObject<EmailIsAlreadyRegisteredError>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.IsType<EmailIsAlreadyRegisteredError>(registerResponse);


    }
    [Fact]
    public async void should_return_ok_when_user_register()
    {
        var client = _factory.CreateClient();
        var request = new UserRegisterDto("erick", "erickjb93@gmail.com", "Sirlei231@");
        var response = await client.PostAsJsonAsync("api/auth/register", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var registerResponse = JsonConvert.DeserializeObject<ResponseAuth>(responseContent);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(201, registerResponse.status);
        Assert.Equal("úsuario cadastrado com sucesso", registerResponse.message);
    }
    private void InitializeDbForTests(AppDbContext db)
    {
        var user = new UserModel("erick", "erickjb93@gmail.com", "$2a$12$TyTP3Zj.VQsDgPbf9h7Tvu7bMR1J8fXDnBo7pXxns0Sz0/3E15VMe")
        {
            externalId = Guid.Parse("04b460bd-001e-482d-8f40-5f329b83de94")
        };
        db.users.Add(user);
        db.SaveChanges();

        var server = new ServerModel("teste22", user.id);
        db.servers.Add(server);
        db.SaveChanges();
    }
}