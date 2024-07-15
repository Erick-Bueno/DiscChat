
using Microsoft.EntityFrameworkCore;

public class GuildRepository : IGuildRepository
{
    private readonly AppDbContext _appDbContext;

    public GuildRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CreateGuild(ServerModel guild)
    {
        await _appDbContext.servers.AddAsync(guild);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteGuild(ServerModel server)
    {
        _appDbContext.servers.Remove(server);
        _appDbContext.SaveChanges();
    }

    public ServerModel FindGuildByExternalId(Guid externalId)
    {
        return _appDbContext.servers.Where(s => s.externalId == externalId).FirstOrDefault();
    }

    public ServerModel FindServerByExternalIdServerAndIdUser(Guid externalIdServer, int userId)
    {
        return _appDbContext.servers.Where(s => s.externalId == externalIdServer && s.idUser == userId).FirstOrDefault();
    }

    public List<GuildLinq> GetAllGuilds()
    {
        return _appDbContext.servers.Select(s => new GuildLinq
        {
            serverName = s.serverName,
            externalId = s.externalId
        }).ToList();
    }
}