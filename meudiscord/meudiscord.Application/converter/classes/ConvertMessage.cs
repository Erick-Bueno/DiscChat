public class ConvertMessage : IConvertMessage
{
    public MessageEntity ConvertInMessageEntity(int idUser, int idChannel, string message)
    {
        return new MessageEntity(){
            idUser = idUser,
            idChannel = idChannel,
            message = message
        };
    }
}