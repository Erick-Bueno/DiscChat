
using Microsoft.AspNetCore.Mvc;
using OneOf;

public class MessageService : IMessageService
{
    private readonly IChannelRepository _channelRepository;
    private readonly IMessageRepository _messageRepository;

    public MessageService(IChannelRepository channelRepository, IMessageRepository messageRepository)
    {
        _channelRepository = channelRepository;
        _messageRepository = messageRepository;
    }

    public async Task<OneOf<ResponseSuccessDefault, AppError>> DeleteMessageInChannel(Guid externalIdChannel, Guid externalIdMessage)
    {
        try
        {
            var channelEntity = _channelRepository.GetChannelByExternalId(externalIdChannel);
            if (channelEntity == null)
                return new ChannelNotFoundError("Canal não encontrado");
            var messageEntity = _messageRepository.GetMessageByChannelIdAndExternalIdMessage(channelEntity.id, externalIdMessage);
            if (messageEntity == null)
                return new MessageNotFoundError("Mensagem não encontrada");
            await _messageRepository.DeleteMessageInChannel(messageEntity);
            return new ResponseSuccessDefault(200, "Mensagem deletada com sucesso");
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }

    }

    public OneOf<ResponseGetOldMessages, AppError> GetOldMessages(Guid externalIdChannel)
    {
        try
        {
            var channel = _channelRepository.GetChannelByExternalId(externalIdChannel);
            if (channel == null)
                return new InvalidChannelError("Canal não encontrado");

            var oldMessages = _messageRepository.GetOldMessages(channel.id);
            return new ResponseGetOldMessages(200, "Mensagens encontradas", oldMessages);
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }

    }
}