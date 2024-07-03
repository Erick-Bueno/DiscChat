public class ResponseAllGuilds : Response
{
    public List<GuildLinq> guilds { get; set; }
    public ResponseAllGuilds(int status, string message, List<GuildLinq> guilds) : base(status, message)
    {
        this.guilds = guilds;
    }
}