public class ResponseNewTokens : Response
{
    public string refreshToken { get; set; }
    public string accessToken { get; set; }
    public ResponseNewTokens(int status, string message, string refreshToken, string accessToken) : base(status, message)
    {
        this.refreshToken = refreshToken;
        this.accessToken = accessToken;
    }
}