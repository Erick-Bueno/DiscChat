using OneOf;

public interface IChannelService{
    public Task<OneOf<ResponseAllChannels,AppError>>  GetAllChannels(Guid externalServerId);
    public Task<OneOf<ResponseCreateChannel,AppError>> CreateChannel (ChannelDto channel);
}