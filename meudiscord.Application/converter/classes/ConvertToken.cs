public class ConvertToken : IConvertToken
{
    public TokenEntity ConvertInTokenEntity(string refreshToken, string email)
    {
        return new TokenEntity(email, refreshToken);
    }
}