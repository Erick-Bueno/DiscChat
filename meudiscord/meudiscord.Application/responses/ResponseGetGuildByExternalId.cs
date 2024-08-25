public class ResponseGetGuildByExternalId : Response
{
    public string guildName { get; set; }
    public ResponseGetGuildByExternalId(int status, string message, string guildName) : base(status, message)
    {
        this.guildName = guildName;
    }
}