
public class ChannelService : IChannelService
{
    private readonly IGuildRepository _guildRepository;
    private readonly IChannelRepository _channelRepository;
    private readonly IConvertChannelDto _convertChannelDto;

    public ChannelService(IGuildRepository guildRepository, IChannelRepository channelRepository, IConvertChannelDto convertChannelDto)
    {
        _guildRepository = guildRepository;
        _channelRepository = channelRepository;
        _convertChannelDto = convertChannelDto;
    }

    public async Task<Response> CreateChannel(ChannelDto channel)
    {
        var server = _guildRepository.FindGuildByExternalId(channel.externalIdServer);
        if(server == null){
            return new ResponseError (400, "Servidor invalido");
        }
        var channelModel = _convertChannelDto.ConvertInChannelModel(channel, server.id);
        var newChannel = await _channelRepository.CreateChannel(channelModel);
        return new ResponseCreateChannel(201, "Servidor criado com sucesso", newChannel.name, newChannel.externalId);
    }

    public async Task<Response> GetAllChannels(Guid externalServerId)
    {
        var server = _guildRepository.FindGuildByExternalId(externalServerId);
        if(server == null){
            return new ResponseError (400, "Servidor invalido");
        }
        var channels = _channelRepository.GetAllChannels(server.id);
        return new ResponseAllChannels(200, "Canais encontrados", channels);
    }
}