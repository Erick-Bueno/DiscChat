using System.Net;
using Moq;
using Xunit;

public class ChannelServiceTest
{
    [Fact]
    public async void should_return_error_invalid_channel_when_create_server()
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

        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(channelDto.externalIdServer)).Returns((ServerModel)null);

        var result = await channelService.CreateChannel(channelDto);
        var response = new ResponseError(400, "Servidor invalido");
        Assert.Equal(response.message, result.message);
        Assert.Equal(response.status, result.status);
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
        var serverModel = new ServerModel("teste", 1)
        {
            id = 1
        };
        var channelModel = new ChannelModel()
        {
            serverId = serverModel.id
        };
        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(channelDto.externalIdServer)).Returns(serverModel);
        convertChannelDtoMock.Setup(cc => cc.ConvertInChannelModel(channelDto, serverModel.id)).Returns(channelModel);
        channelRepositoryMock.Setup(cr => cr.CreateChannel(channelModel)).ReturnsAsync(channelModel);

        var result = await channelService.CreateChannel(channelDto);
        var response = new ResponseCreateChannel(201, "Servidor criado com sucesso", channelModel.name, channelModel.externalId);
        Assert.Equal(response.message, result.message);
        Assert.Equal(response.status, result.status);
    }
    [Fact]
    public async void should_return_error_invalid_channel_when_get_all_channels()
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

        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(channelDto.externalIdServer)).Returns((ServerModel)null);

        var result = await channelService.GetAllChannels(channelDto.externalIdServer);
        var response = new ResponseError(400, "Servidor invalido");
        Assert.Equal(response.message, result.message);
        Assert.Equal(response.status, result.status);
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
        var serverModel = new ServerModel("teste", 1)
        {
            id = 1
        };
        var channelModel = new ChannelModel()
        {
            serverId = serverModel.id
        };
        var channelLinq = new GetAllChannelsLinq();
        var listChannels = new List<GetAllChannelsLinq>(){
            channelLinq
        };
        guildRepositoryMock.Setup(gr => gr.FindGuildByExternalId(channelDto.externalIdServer)).Returns(serverModel);
        channelRepositoryMock.Setup(cr => cr.GetAllChannels(serverModel.id)).Returns(listChannels);
        var result = await channelService.GetAllChannels(channelDto.externalIdServer) as ResponseAllChannels;
        var response = new ResponseAllChannels(200, "Canais encontrados", listChannels);
        Assert.Equal(response.message, result.message);
        Assert.Equal(response.status, result.status);
        Assert.Equal(response.channels, result.channels);
    }
}