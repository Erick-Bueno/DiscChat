using System.Net;
using Moq;
using OneOf;
using Xunit;

public class ChannelServiceTest
{
    [Fact]
    public async void should_return_error_invalid_server_when_create_channel()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var channelRepositoryMock = new Mock<IChannelRepository>();
        var convertChannelDtoMock = new Mock<IConvertChannelDto>();

        var channelService = new ChannelService(guildRepositoryMock.Object, channelRepositoryMock.Object, convertChannelDtoMock.Object);

        var channelDto = new ChannelDto()
        {
            channelName = "teste",
            externalIdServer = Guid.NewGuid()
        };

        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(channelDto.externalIdServer)).Returns((ServerEntity)null);

        var result = await channelService.CreateChannel(channelDto);
        Assert.IsType<InvalidServerError>(result.AsT1);
    }
    [Fact]
    public async void should_create_channel_when_create_channel()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var channelRepositoryMock = new Mock<IChannelRepository>();
        var convertChannelDtoMock = new Mock<IConvertChannelDto>();

        var channelService = new ChannelService(guildRepositoryMock.Object, channelRepositoryMock.Object, convertChannelDtoMock.Object);

        var channelDto = new ChannelDto()
        {
            channelName = "teste",
            externalIdServer = Guid.NewGuid(),
        };
        var serverModel = new ServerEntity("teste", 1)
        {
            id = 1
        };
        var channelModel = new ChannelEntity()
        {
            serverId = serverModel.id
        };
        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(channelDto.externalIdServer)).Returns(serverModel);
        convertChannelDtoMock.Setup(cc => cc.ConvertInChannelEntity(channelDto, serverModel.id)).Returns(channelModel);
        channelRepositoryMock.Setup(cr => cr.CreateChannel(channelModel)).ReturnsAsync(channelModel);

        var result = await channelService.CreateChannel(channelDto);
        var response = new ResponseCreateChannel(201, "Servidor criado com sucesso", channelModel.name, channelModel.externalId);
        Assert.Equal(response.message, result.AsT0.message);
        Assert.Equal(response.status, result.AsT0.status);
    }
    [Fact]
    public async void should_return_error_invalid_server_when_get_all_channels()
    {

        var guildRepositoryMock = new Mock<IGuildRepository>();
        var channelRepositoryMock = new Mock<IChannelRepository>();
        var convertChannelDtoMock = new Mock<IConvertChannelDto>();

        var channelService = new ChannelService(guildRepositoryMock.Object, channelRepositoryMock.Object, convertChannelDtoMock.Object);

        var channelDto = new ChannelDto()
        {
            channelName = "teste",
            externalIdServer = Guid.NewGuid()
        };

        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(channelDto.externalIdServer)).Returns((ServerEntity)null);

        var result = await channelService.GetAllChannels(channelDto.externalIdServer);
        Assert.IsType<InvalidServerError>(result.AsT1);
    }
    [Fact]
    public async void should_list_all_channels_when_get_all_channels()
    {
        var guildRepositoryMock = new Mock<IGuildRepository>();
        var channelRepositoryMock = new Mock<IChannelRepository>();
        var convertChannelDtoMock = new Mock<IConvertChannelDto>();

        var channelService = new ChannelService(guildRepositoryMock.Object, channelRepositoryMock.Object, convertChannelDtoMock.Object);

        var channelDto = new ChannelDto()
        {
            channelName = "teste",
            externalIdServer = Guid.NewGuid(),
        };
        var serverModel = new ServerEntity("teste", 1)
        {
            id = 1
        };
        var channelModel = new ChannelEntity()
        {
            serverId = serverModel.id
        };
        var channelLinq = new GetAllChannelsLinq();
        var listChannels = new List<GetAllChannelsLinq>(){
            channelLinq
        };
        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(channelDto.externalIdServer)).Returns(serverModel);
        channelRepositoryMock.Setup(cr => cr.GetAllChannels(serverModel.id)).Returns(listChannels);
        var result = await channelService.GetAllChannels(channelDto.externalIdServer);
        var response = new ResponseAllChannels(200, "Canais encontrados", listChannels);
        Assert.Equal(response.message, result.AsT0.message);
        Assert.Equal(response.status, result.AsT0.status);
        Assert.Equal(response.channels, result.AsT0.channels);
    }
}