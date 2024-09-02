using System.Threading.Channels;

public interface IChannelRepository
{
    public List<GetAllChannelsLinq> GetAllChannels(int serverId);
    public Task<ChannelEntity> CreateChannel(ChannelEntity channel);
    public ChannelEntity GetChannelByExternalId(Guid externalId);
}