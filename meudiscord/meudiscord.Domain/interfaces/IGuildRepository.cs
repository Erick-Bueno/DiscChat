public interface IGuildRepository
{
    public Task<List<GuildLinq>> GetAllGuilds();
    public Task CreateGuild(ServerModel guild);
    public Task DeleteGuild (ServerModel server);
    public ServerModel FindServerByExternalIdServerAndIdUser(Guid externalIdServer, int userId);
} 