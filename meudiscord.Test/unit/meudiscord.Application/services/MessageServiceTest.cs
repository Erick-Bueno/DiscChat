using System.Security.Authentication.ExtendedProtection;
using System.Threading.Channels;
using Moq;
using Xunit;

public class MessageServiceTest
{
  [Fact]
  public async void should_return_error_invalid_channel_when_get_old_messages()
  {
    var channelRepositoryMock = new Mock<IChannelRepository>();
    var messageRepositoryMock = new Mock<IMessageRepository>();

    var messageService = new MessageService(channelRepositoryMock.Object, messageRepositoryMock.Object);

    var externalIdChannel = Guid.NewGuid();

    channelRepositoryMock.Setup(cr => cr.GetChannelByExternalId(externalIdChannel)).Returns((ChannelEntity)null);

    var result = messageService.GetOldMessages(externalIdChannel);
    Assert.IsType<InvalidChannelError>(result.AsT1);
  }
  [Fact]
  public async void should_return_old_messages_when_get_old_messages()
  {
    var channelRepositoryMock = new Mock<IChannelRepository>();
    var messageRepositoryMock = new Mock<IMessageRepository>();

    var messageService = new MessageService(channelRepositoryMock.Object, messageRepositoryMock.Object);

    var channelModel = new ChannelEntity()
    {
      id = 1,
      name = "teste",
      externalId = Guid.NewGuid()
    };
    var listMessagesLinq = new List<MessagesLinq>();

    channelRepositoryMock.Setup(cr => cr.GetChannelByExternalId(channelModel.externalId)).Returns(channelModel);

    var response = new ResponseGetOldMessages(200, "Mensagens encontradas", listMessagesLinq);

    var result = messageService.GetOldMessages(channelModel.externalId);

    Assert.Equal(response.message, result.AsT0.message);
    Assert.Equal(response.status, result.AsT0.status);

  }
  [Fact]
  public async void should_return_error_channel_not_found_when_delete_message_in_channel()
  {
    var channelRepositoryMock = new Mock<IChannelRepository>();
    var messageRepositoryMock = new Mock<IMessageRepository>();

    var messageService = new MessageService(channelRepositoryMock.Object, messageRepositoryMock.Object);

    var channelModel = new ChannelEntity()
    {
      id = 1,
      externalId = Guid.NewGuid()
    };
    var messageModel = new MessageEntity()
    {
      id = 1,
      externalId = Guid.NewGuid()
    };

    channelRepositoryMock.Setup(cr => cr.GetChannelByExternalId(channelModel.externalId)).Returns((ChannelEntity)null);

    var result = await messageService.DeleteMessageInChannel(channelModel.externalId, messageModel.externalId);

    Assert.IsType<ChannelNotFoundError>(result.AsT1);
  }
  [Fact]
  public async void should_return_error_message_not_found_when_delete_message_in_channel()
  {
    var channelRepositoryMock = new Mock<IChannelRepository>();
    var messageRepositoryMock = new Mock<IMessageRepository>();

    var messageService = new MessageService(channelRepositoryMock.Object, messageRepositoryMock.Object);

    var channelModel = new ChannelEntity()
    {
      id = 1,
      externalId = Guid.NewGuid()
    };
    var messageModel = new MessageEntity()
    {
      id = 1,
      externalId = Guid.NewGuid()
    };

    channelRepositoryMock.Setup(cr => cr.GetChannelByExternalId(channelModel.externalId)).Returns(channelModel);
    messageRepositoryMock.Setup(mr => mr.GetMessageByChannelIdAndExternalIdMessage(channelModel.id, messageModel.externalId)).Returns((MessageEntity)null);

    var result = await messageService.DeleteMessageInChannel(channelModel.externalId, messageModel.externalId);
    Assert.IsType<MessageNotFoundError>(result.AsT1);
  }
  [Fact]
  public async void should_delete_message_when_delete_message_in_channel()
  {
    var channelRepositoryMock = new Mock<IChannelRepository>();
    var messageRepositoryMock = new Mock<IMessageRepository>();

    var messageService = new MessageService(channelRepositoryMock.Object, messageRepositoryMock.Object);

    var channelModel = new ChannelEntity()
    {
      id = 1,
      externalId = Guid.NewGuid()
    };
    var messageModel = new MessageEntity()
    {
      id = 1,
      externalId = Guid.NewGuid()
    };

    channelRepositoryMock.Setup(cr => cr.GetChannelByExternalId(channelModel.externalId)).Returns(channelModel);
    messageRepositoryMock.Setup(mr => mr.GetMessageByChannelIdAndExternalIdMessage(channelModel.id, messageModel.externalId)).Returns(messageModel);
    messageRepositoryMock.Setup(mr => mr.DeleteMessageInChannel(messageModel));
    var result = await messageService.DeleteMessageInChannel(channelModel.externalId, messageModel.externalId);

    var response = new ResponseSuccessDefault(200, "Mensagem deletada com sucesso");

    Assert.Equal(response.message, result.AsT0.message);
    Assert.Equal(response.status, result.AsT0.status);
  }
}