public class ResponseGetOldMessages : Response
{
    public List<MessagesLinq> messagesData;
    public ResponseGetOldMessages(int status, string message, List<MessagesLinq> messagesData) : base(status, message)
    {
        this.messagesData = messagesData;
    }
}