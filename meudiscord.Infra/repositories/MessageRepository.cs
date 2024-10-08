

using Microsoft.EntityFrameworkCore;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _appDbContext;

    public MessageRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CreateMessage(MessageEntity message)
    {
        await _appDbContext.messages.AddAsync(message);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteMessageInChannel(MessageEntity message)
    {
        _appDbContext.messages.Remove(message);
        _appDbContext.SaveChanges();
    }

    public MessageEntity GetMessageByChannelIdAndExternalIdMessage(int channelId, Guid externalIdMessage)
    {
        return _appDbContext.messages.Where(m => m.idChannel == channelId && m.externalId == externalIdMessage).FirstOrDefault();
    }

    public List<MessagesLinq> GetOldMessages(int idChannel)
    {
        var userMessage = (
        from message in _appDbContext.messages
        join user in _appDbContext.users on message.idUser equals user.id
        where message.idChannel == idChannel
        orderby message.createdAt ascending
        select new MessagesLinq
        {
            externalIdMessage = message.externalId,
            externalIdUser = user.externalId,
            message = message.message,
            userName = user.name,
        }).ToList();
        return userMessage;

    }
}