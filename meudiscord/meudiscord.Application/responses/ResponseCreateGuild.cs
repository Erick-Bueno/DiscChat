public class ResponseCreateGuild : Response
{
    public Guid externalId { get; set; }
    public ResponseCreateGuild(int status, string message, Guid externalId) : base(status, message)
    {
        this.externalId = externalId;
    }
}