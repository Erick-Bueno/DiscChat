public class ResponseUserData : Response
{
    public string name { get;set; }
    public string email { get;set; }
    public ResponseUserData(int status, string message, string name, string email) : base(status, message)
    {
        this.name = name;
        this.email = email;
    }
}