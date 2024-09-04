public interface IGuildRepository
{
    public List<GuildLinq> GetAllGuilds();
    public Task CreateGuild(ServerEntity guild);
    public Task DeleteGuild (ServerEntity server);
    public ServerEntity FindServerByExternalIdServerAndIdUser(Guid externalIdServer, int userId);
    public ServerEntity FindGuildByExternalId(Guid externalId);
} 