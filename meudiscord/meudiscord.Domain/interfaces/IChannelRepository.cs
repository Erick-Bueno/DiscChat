using System.Threading.Channels;

public interface IChannelRepository
{
    public List<ChannelModel> GetAllChannels(int serverId);
    public Task<ChannelModel> CreateChannel(ChannelModel channel);
    public ChannelModel GetChannelByExternalId(Guid externalId);
}