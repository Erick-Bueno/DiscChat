using OneOf;

public interface IMessageService
{
    public OneOf<ResponseGetOldMessages,AppError> GetOldMessages(Guid externalIdChannel);
    public Task<OneOf<ResponseSuccessDefault, AppError>> DeleteMessageInChannel(Guid externalIdChannel, Guid externalIdMessage);
}