public interface ITokenRepository{
    public Task<TokenEntity> RegisterToken(TokenEntity token);
    public Task UpdateToken (string email, string newRefreshToken);
    public TokenEntity FindUserDataByToken(string refreshToken);
}