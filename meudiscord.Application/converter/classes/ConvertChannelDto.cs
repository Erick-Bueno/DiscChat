public class ConvertChannelDto : IConvertChannelDto
{
    public ChannelEntity ConvertInChannelEntity(ChannelDto channel, int serverId)
    {
        return new ChannelEntity(){
            serverId = serverId,
            name = channel.channelName
        };
    }
}