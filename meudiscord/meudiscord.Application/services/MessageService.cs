
using Microsoft.AspNetCore.Mvc;

public class MessageService : IMessageService
{
    private readonly IChannelRepository _channelRepository;
    private readonly IMessageRepository _messageRepository;

    public MessageService(IChannelRepository channelRepository, IMessageRepository messageRepository)
    {
        _channelRepository = channelRepository;
        _messageRepository = messageRepository;
    }

    public async Task<Response> DeleteMessageInChannel(Guid externalIdChannel, Guid externalIdMessage)
    {
        var channelModel = _channelRepository.GetChannelByExternalId(externalIdChannel);
        if (channelModel == null){
            return new ResponseError(400, "Canal não encontrado");
        }
        var messageModel = _messageRepository.GetMessageByChannelIdAndExternalIdMessage(channelModel.id, externalIdMessage);
        if (messageModel == null){
            return new ResponseError(400, "Mensagem não encontrada");
        }
        await _messageRepository.DeleteMessageInChannel(messageModel);
        return new ResponseSuccessDefault(200, "Mensagem deletada com sucesso");
    }
    
    public Response GetOldMessages(Guid externalIdChannel)
    {
        var channel = _channelRepository.GetChannelByExternalId(externalIdChannel);
        if(channel == null){
            return new ResponseError(400, "Canal invalido");
        }
        var oldMessages =  _messageRepository.GetOldMessages(channel.id);
        return new ResponseGetOldMessages(200, "Mensagens encontradas", oldMessages);
    }
}