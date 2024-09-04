public interface IMessageRepository
{
    public List<MessagesLinq> GetOldMessages(int id);
    public Task CreateMessage(MessageEntity message);
    public Task DeleteMessageInChannel(MessageEntity message);
    public MessageEntity GetMessageByChannelIdAndExternalIdMessage(int channelId, Guid externalIdMessage);
}