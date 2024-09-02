
using OneOf;

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

    public async Task<OneOf<ResponseCreateChannel, AppError>> CreateChannel(ChannelDto channel)
    {
        try
        {
            var server = _guildRepository.FindGuildByExternalId(channel.externalIdServer);
            if (server == null)
                return new InvalidServerError("Servidor Invalido");

            var ChannelEntity = _convertChannelDto.ConvertInChannelEntity(channel, server.id);
            var newChannel = await _channelRepository.CreateChannel(ChannelEntity);
            return new ResponseCreateChannel(201, "Servidor criado com sucesso", newChannel.name, newChannel.externalId);
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }

    }

    public async Task<OneOf<ResponseAllChannels, AppError>> GetAllChannels(Guid externalServerId)
    {
        try
        {
            var server = _guildRepository.FindGuildByExternalId(externalServerId);
            if (server == null)
                return new InvalidServerError("Servidor Invalido");
            var channels = _channelRepository.GetAllChannels(server.id);
            return new ResponseAllChannels(200, "Canais encontrados", channels);
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }

    }

}