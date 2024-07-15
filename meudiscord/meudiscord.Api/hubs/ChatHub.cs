using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;

public class ChatHub:Hub
{
    private readonly IUserRepository _userRepository;
    private readonly IChannelRepository _channelRepository;
    private readonly IConvertMessage _convertMessage;
    private readonly IMessageRepository _messageRepository;

    public ChatHub(IUserRepository userRepository, IChannelRepository channelRepository, IConvertMessage convertMessage, IMessageRepository messageRepository)
    {
        _userRepository = userRepository;
        _channelRepository = channelRepository;
        _convertMessage = convertMessage;
        _messageRepository = messageRepository;
    }

    public async Task SendMessage(string message, Guid externalIdUser, Guid externalIdChannel){
        var user = _userRepository.FindUserByExternalId(externalIdUser);
        var channel = _channelRepository.GetChannelByExternalId(externalIdChannel);
        var messageModel = _convertMessage.ConvertInMessageModel(user.id, channel.id, message);
        await _messageRepository.CreateMessage(messageModel);
        
        await Clients.Group(externalIdChannel.ToString()).SendAsync("ReceiverMessage", message, user.name);
    }
    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var externalIdChannel = Context.GetHttpContext().Request.Query["externalIdChannel"];
        await Groups.AddToGroupAsync(connectionId, externalIdChannel.ToString());
        await base.OnConnectedAsync();
    }
    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        var externalIdChannel = Context.GetHttpContext().Request.Query["externalIdChannel"];
        await Groups.RemoveFromGroupAsync(connectionId, externalIdChannel.ToString());
        await base.OnDisconnectedAsync(exception);
    }
}