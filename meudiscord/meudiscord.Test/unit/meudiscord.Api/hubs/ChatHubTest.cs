using Microsoft.AspNetCore.SignalR;
using Moq;
using Xunit;

public class ChatHubTest
{
    [Fact]
    public async void should_send_message_to_group()
    {
       var userRepositoryMock = new Mock<IUserRepository>();
       var channelRepositoryMock = new Mock<IChannelRepository>();
       var convertMessageMock = new Mock <IConvertMessage>();
       var messageRepositoryMock = new Mock<IMessageRepository>();
       var clientsMock = new Mock<IHubCallerClients>();
       var clientProxyMock = new Mock<IClientProxy>();

       var userModel = new UserEntity("erick", "erickjb93@gmail.com", "sirlei231"){
        externalId = Guid.NewGuid(),
        id = 1
       };
       var channelModel = new ChannelEntity {
        externalId = Guid.NewGuid(),
        id = 1
       };
       var userLinq = new UserLinq(){
        id = 1,
       };
       var messageModel = new MessageEntity();

       clientsMock.Setup(c => c.Group(channelModel.externalId.ToString())).Returns(clientProxyMock.Object);
       userRepositoryMock.Setup(u => u.FindUserByExternalId(userModel.externalId)).Returns(userLinq);
       channelRepositoryMock.Setup(c => c.GetChannelByExternalId(channelModel.externalId)).Returns(channelModel);
       convertMessageMock.Setup(cm => cm.ConvertInMessageEntity(userModel.id, channelModel.id, "teste")).Returns(messageModel);
       messageRepositoryMock.Setup(mr => mr.CreateMessage(messageModel));

       var chatHub = new ChatHub(userRepositoryMock.Object, channelRepositoryMock.Object, convertMessageMock.Object, messageRepositoryMock.Object);

       chatHub.Clients = clientsMock.Object;

       await chatHub.SendMessage("teste", userModel.externalId, channelModel.externalId);

        clientProxyMock.Verify(cp => cp.SendCoreAsync("ReceiverMessage", It.IsAny<object[]>(), default), Times.Once);
    }
}