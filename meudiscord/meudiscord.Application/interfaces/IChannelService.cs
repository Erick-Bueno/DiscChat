public interface IChannelService{
    public Task<Response>  GetAllChannels(Guid externalServerId);
    public Task<Response> CreateChannel (ChannelDto channel);
}