public class ConvertToken : IConvertToken
{
    public TokenModel ConvertInTokenModel(string refreshToken, string email)
    {
        return new TokenModel(email, refreshToken);
    }
}