public interface IConvertMessage
{
    public MessageModel ConvertInMessageModel(int idUser, int idChannel, string message);
}