public interface IMessageService
{
    public Response GetOldMessages(Guid externalIdChannel);
    public Task<Response> DeleteMessageInChannel(Guid externalIdChannel, Guid externalIdMessage);
}