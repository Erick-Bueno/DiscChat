using Moq;
using Xunit;

public class GuildServiceTest
{
    [Fact]
    public async void should_return_error_user_invalid_when_create_guild()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var convertGuildDtoMock = new Mock<IConvertGuildDto>();

        var guildService = new GuildService(guildRepositoryMock.Object, userRepositoryMock.Object, convertGuildDtoMock.Object);

        var guildDto = new GuildDto()
        {
            externalIdUser = Guid.NewGuid(),
            serverName = "teste"
        };
        userRepositoryMock.Setup(ur => ur.FindUserByExternalId(guildDto.externalIdUser)).Returns((UserLinq)null);

        var result = await guildService.CreateGuild(guildDto);
        Assert.IsType<UnableToCreateServerError>(result.AsT1);
    }
    [Fact]
    public async void should_create_a_guild()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var convertGuildDtoMock = new Mock<IConvertGuildDto>();

        var guildService = new GuildService(guildRepositoryMock.Object, userRepositoryMock.Object, convertGuildDtoMock.Object);

        var guildDto = new GuildDto()
        {
            externalIdUser = Guid.NewGuid(),
            serverName = "teste"
        };
        var userLinq = new UserLinq()
        {
            email = "erickjb93@gmail.com",
            id = 1,
            name = "erick"
        };
        var serverModel = new ServerEntity(guildDto.serverName, userLinq.id);
        userRepositoryMock.Setup(ur => ur.FindUserByExternalId(guildDto.externalIdUser)).Returns(userLinq);
        convertGuildDtoMock.Setup(cg => cg.ConvertInServerEntity(guildDto, userLinq.id)).Returns(serverModel);
        guildRepositoryMock.Setup(gp => gp.CreateGuild(serverModel));
        var result = await guildService.CreateGuild(guildDto);
        var response = new ResponseCreateGuild(201, "Servidor criado com sucesso", serverModel.externalId, serverModel.serverName);
        Assert.Equal(response.message, result.AsT0.message);
        Assert.Equal(response.status, result.AsT0.status);
    }
    [Fact]
    public async void should_return_error_user_invalid_when_delete_guild()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var convertGuildDtoMock = new Mock<IConvertGuildDto>();

        var guildService = new GuildService(guildRepositoryMock.Object, userRepositoryMock.Object, convertGuildDtoMock.Object);

        var deleteGuildDto = new DeleteGuildDto(Guid.NewGuid(), Guid.NewGuid());
        userRepositoryMock.Setup(ur => ur.FindUserByExternalId(deleteGuildDto.externalIdUser)).Returns((UserLinq)null);

        var result = await guildService.DeleteGuild(deleteGuildDto);
        Assert.IsType<UnableToDeleteServerError>(result.AsT1);

    }
    [Fact]
    public async void should_return_error_guild_does_not_belong_to_the_user_when_delete_guild()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var convertGuildDtoMock = new Mock<IConvertGuildDto>();

        var guildService = new GuildService(guildRepositoryMock.Object, userRepositoryMock.Object, convertGuildDtoMock.Object);

        var userLinq = new UserLinq()
        {
            email = "erickjb93@gmail.com",
            id = 1,
            name = "erick"
        };
        var deleteGuildDto = new DeleteGuildDto(Guid.NewGuid(), Guid.NewGuid());
        userRepositoryMock.Setup(ur => ur.FindUserByExternalId(deleteGuildDto.externalIdUser)).Returns(userLinq);
        guildRepositoryMock.Setup(gr => gr.FindServerByExternalIdServerAndIdUser(deleteGuildDto.externalIdServer, userLinq.id)).Returns((ServerEntity)null);

        var result = await guildService.DeleteGuild(deleteGuildDto);
        Assert.IsType<TheServerDoesNotBelongToTheUserTryingToDeleItError>(result.AsT1);
    }
    [Fact]
    public async void should_delete_the_guild()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var convertGuildDtoMock = new Mock<IConvertGuildDto>();

        var guildService = new GuildService(guildRepositoryMock.Object, userRepositoryMock.Object, convertGuildDtoMock.Object);

        var userLinq = new UserLinq()
        {
            email = "erickjb93@gmail.com",
            id = 1,
            name = "erick"
        };
        var deleteGuildDto = new DeleteGuildDto(Guid.NewGuid(), Guid.NewGuid());
        var serverModel = new ServerEntity("teste", userLinq.id);
        userRepositoryMock.Setup(ur => ur.FindUserByExternalId(deleteGuildDto.externalIdUser)).Returns(userLinq);
        guildRepositoryMock.Setup(gr => gr.FindServerByExternalIdServerAndIdUser(deleteGuildDto.externalIdServer, userLinq.id)).Returns(serverModel);
        guildRepositoryMock.Setup(gr => gr.DeleteGuild(serverModel));
        var response = new ResponseSuccessDefault(200, "Servidor deletado com sucesso");
        var result = await guildService.DeleteGuild(deleteGuildDto);
        Assert.Equal(response.status, result.AsT0.status);
        Assert.Equal(response.message, result.AsT0.message);

    }
    [Fact]
    public async void should_return_error_no_guilds_found_when_get_all_guilds()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var convertGuildDtoMock = new Mock<IConvertGuildDto>();

        var guildService = new GuildService(guildRepositoryMock.Object, userRepositoryMock.Object, convertGuildDtoMock.Object);

        List<GuildLinq>? guildList = new List<GuildLinq>();
        guildRepositoryMock.Setup(gr => gr.GetAllGuilds()).Returns(guildList);
        var result = await guildService.GetAllGuilds();
        Assert.IsType<NoServersWereFoundError>(result.AsT1);
    }
    [Fact]
    public async void should_get_all_guilds()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var convertGuildDtoMock = new Mock<IConvertGuildDto>();

        var guildService = new GuildService(guildRepositoryMock.Object, userRepositoryMock.Object, convertGuildDtoMock.Object);

        var listGuildLinq = new List<GuildLinq>
        {
            new GuildLinq(){externalId = Guid.NewGuid(), serverName = "teste"}
        };

        guildRepositoryMock.Setup(gr => gr.GetAllGuilds()).Returns(listGuildLinq);

        var result = await guildService.GetAllGuilds();
        var response = new ResponseAllGuilds(200, "Guildas encontradas", listGuildLinq);
        Assert.Equal(response.status, result.AsT0.status);
        Assert.Equal(response.message, result.AsT0.message);
        Assert.Equal(response.guilds, result.AsT0.guilds);
        Assert.Equal(response.guilds.Count, 1);
    }
    [Fact]
    public async void should_return_error_server_not_found_error_when_get_guild_by_external_id()
    {

        var guildRepositoryMock = new Mock<IGuildRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var convertGuildDtoMock = new Mock<IConvertGuildDto>();
        var externalIdServer = Guid.NewGuid();
        var guildService = new GuildService(guildRepositoryMock.Object, userRepositoryMock.Object, convertGuildDtoMock.Object);

        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(externalIdServer)).Returns((ServerEntity)null);
        var result = await guildService.GetGuildByExternalId(externalIdServer);
        Assert.IsType<ServerNotFoundError>(result.AsT1);
    }
    [Fact]
    public async void should_get_guild_by_external_id()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var convertGuildDtoMock = new Mock<IConvertGuildDto>();
        var serverModel = new ServerEntity("teste",1);
        serverModel.externalId = Guid.NewGuid();
        var guildService = new GuildService(guildRepositoryMock.Object, userRepositoryMock.Object, convertGuildDtoMock.Object);

        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(serverModel.externalId)).Returns(serverModel);
        var result = await guildService.GetGuildByExternalId(serverModel.externalId);
        var response = new ResponseGetGuildByExternalId(200, "Servidor encontrado", serverModel.serverName);
        Assert.Equal(response.status, result.AsT0.status);
        Assert.Equal(response.message, result.AsT0.message);
        Assert.Equal(response.guildName, result.AsT0.guildName);
    }
}