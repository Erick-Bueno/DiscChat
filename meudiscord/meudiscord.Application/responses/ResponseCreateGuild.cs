public class ResponseCreateGuild : Response
{
    public Guid externalId { get; set; }
    public string serverName { get; set; }


    public ResponseCreateGuild(int status, string message, Guid externalId, global::System.String serverName) : base(status, message)
    {
        this.externalId = externalId;
        this.serverName = serverName;
    }
}