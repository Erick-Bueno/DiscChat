using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;

public class ChannelRepository : IChannelRepository
{
    private readonly AppDbContext _appDbContext;

    public ChannelRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ChannelModel> CreateChannel(ChannelModel channel)
    {
        await _appDbContext.channels.AddAsync(channel);
        await _appDbContext.SaveChangesAsync();
        return channel;
    }

    public List<ChannelModel> GetAllChannels(int serverId)
    {
        return _appDbContext.channels.Where(c => c.serverId == serverId).ToList();
    }

    public ChannelModel GetChannelByExternalId(Guid externalId)
    {
        return _appDbContext.channels.Where(c => c.externalId == externalId).FirstOrDefault();
    }
}