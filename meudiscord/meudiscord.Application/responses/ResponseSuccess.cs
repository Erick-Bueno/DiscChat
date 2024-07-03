public class ResponseAuth : Response
{
    public string refreshToken { get; set; }
    public string accesstoken { get; set; }

    public ResponseAuth(int status, string message, string refreshToken, string accesstoken) : base(status, message)
    {
        this.refreshToken = refreshToken;
        this.accesstoken = accesstoken;
    }
}