using System.Security.Cryptography.X509Certificates;
using System.Text;
using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

public class JwtServiceTest
{

    [Fact]
  public void should_return_false_when_validate_token()
  {
    var tokenRepositoryMock = new Mock<ITokenRepository>();
    var userRepositoryMock = new Mock<IUserRepository>();

    var userModel = new UserEntity("teste", "teste", "teste");

    var inMemorySettings = new Dictionary<string, string> {
            {"KeyRefreshToken", "adsdasdasdasdads"} // Substitua pela sua chave de teste
        };

    Microsoft.Extensions.Configuration.IConfiguration configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(inMemorySettings)
        .Build();
    var jwtService = new JwtService(configuration, tokenRepositoryMock.Object, userRepositoryMock.Object);

    var result = jwtService.ValidateToken("vhgnhvnvhn");

    Assert.False(result);

  }
  [Fact]
  public void should_return_true_when_validate_token()
  {
    var keyRefreshToken = Environment.GetEnvironmentVariable("SECRET_KEY_REFRESH_TOKEN");
    var tokenRepositoryMock = new Mock<ITokenRepository>();
    var userRepositoryMock = new Mock<IUserRepository>();

    var userModel = new UserEntity("teste", "teste", "teste");

    var inMemorySettings = InMemorySettingsConfiguration.GetMemorySettings();

    Microsoft.Extensions.Configuration.IConfiguration configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(inMemorySettings)
        .Build();
    var jwtService = new JwtService(configuration, tokenRepositoryMock.Object, userRepositoryMock.Object);
    var token = jwtService.GenerateRefreshToken();
    var result = jwtService.ValidateToken(token);

    Assert.True(result);
  }
}