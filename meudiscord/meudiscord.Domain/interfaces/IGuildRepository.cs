public interface IGuildRepository
{
    public List<GuildLinq> GetAllGuilds();
    public Task CreateGuild(ServerModel guild);
    public Task DeleteGuild (ServerModel server);
    public ServerModel FindServerByExternalIdServerAndIdUser(Guid externalIdServer, int userId);
    public ServerModel FindGuildByExternalId(Guid externalId);
} 