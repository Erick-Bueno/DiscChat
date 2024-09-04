public class ResponseCreateChannel : Response
{
    public string channelName { get; set;}
    public Guid externalIdChannel { get; set;}
    public ResponseCreateChannel(int status, string message, string channelName, Guid externalIdChannel) : base(status, message)
    {
        this.channelName = channelName;
        this.externalIdChannel = externalIdChannel;
    }
}