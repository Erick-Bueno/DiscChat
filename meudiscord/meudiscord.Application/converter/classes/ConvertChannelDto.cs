public class ConvertChannelDto : IConvertChannelDto
{
    public ChannelModel ConvertInChannelModel(ChannelDto channel, int serverId)
    {
        return new ChannelModel(){
            serverId = serverId,
            name = channel.channelName
        };
    }
}