public interface IConvertMessage
{
    public MessageEntity ConvertInMessageEntity(int idUser, int idChannel, string message);
}