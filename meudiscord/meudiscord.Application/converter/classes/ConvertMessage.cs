public class ConvertMessage : IConvertMessage
{
    public MessageModel ConvertInMessageModel(int idUser, int idChannel, string message)
    {
        return new MessageModel(){
            idUser = idUser,
            idChannel = idChannel,
            message = message
        };
    }
}