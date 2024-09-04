public class ResponseAllChannels : Response
{
    public List<GetAllChannelsLinq> channels { get; set; }
    public ResponseAllChannels(int status, string message, List<GetAllChannelsLinq> channels) : base(status, message)
    {
        this.channels = channels;
    }
}