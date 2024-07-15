public class ResponseAllChannels : Response
{
    public List<ChannelModel> channels = new List<ChannelModel>();
    public ResponseAllChannels(int status, string message, List<ChannelModel> channels) : base(status, message)
    {
        this.channels = channels;
    }
}