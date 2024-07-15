public interface IMessageRepository
{
    public List<MessagesLinq> GetOldMessages(int id);
    public Task CreateMessage(MessageModel message);
    public Task DeleteMessageInChannel(MessageModel message);
    public MessageModel GetMessageByChannelIdAndExternalIdMessage(int channelId, Guid externalIdMessage);
}