
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
        var channelModel = _channelRepository.GetChannelByExternalId(externalIdChannel);
        if (channelModel == null)
            return new ChannelNotFoundError();
        var messageModel = _messageRepository.GetMessageByChannelIdAndExternalIdMessage(channelModel.id, externalIdMessage);
        if (messageModel == null)
            return new MessageNotFoundError();
        await _messageRepository.DeleteMessageInChannel(messageModel);
        return new ResponseSuccessDefault(200, "Mensagem deletada com sucesso");
    }
    
    public OneOf<ResponseGetOldMessages,AppError> GetOldMessages(Guid externalIdChannel)
    {
        var channel = _channelRepository.GetChannelByExternalId(externalIdChannel);
        if(channel == null)
            return new InvalidChannelError();
    
        var oldMessages =  _messageRepository.GetOldMessages(channel.id);
        return new ResponseGetOldMessages(200, "Mensagens encontradas", oldMessages);
    }
}