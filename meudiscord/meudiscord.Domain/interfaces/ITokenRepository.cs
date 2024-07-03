public interface ITokenRepository{
    public Task<TokenModel> RegisterToken(TokenModel token);
    public Task UpdateToken (string email, string newRefreshToken);
}