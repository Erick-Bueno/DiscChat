using System.Runtime.InteropServices;
using Moq;
using Xunit;

public class UserServiceTest
{
    [Fact]
    public async void should_return_error_user_not_found_when_find_user_authenticated()
    {
       var userRepositoryMock = new Mock<IUserRepository>();
       var userService = new UserService(userRepositoryMock.Object);
       var externalId = Guid.NewGuid();
       userRepositoryMock.Setup(ur => ur.FindUserByExternalId(externalId)).Returns((UserLinq)null);
       
       var result = await userService.FindUserAuthenticated(externalId);

       Assert.IsType<UserNotFoundError>(result.AsT1);
    }
    [Fact]
    public async void should_find_user_authenticated()
    {
       var userRepositoryMock = new Mock<IUserRepository>();
       var userService = new UserService(userRepositoryMock.Object);
       var externalId = Guid.NewGuid();
       var userLinq = new UserLinq();
       userRepositoryMock.Setup(ur => ur.FindUserByExternalId(externalId)).Returns(userLinq);
       var response = new ResponseUserData(200, "Usuário encontrado", userLinq.name, userLinq.email);
       var result = await userService.FindUserAuthenticated(externalId);

       Assert.Equal(response.status, result.AsT0.status);
       Assert.Equal(response.message, result.AsT0.message);
    }
}