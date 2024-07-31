
using Microsoft.EntityFrameworkCore;

public class TokenRepository : ITokenRepository
{
    private readonly AppDbContext _appDbContext;

    public TokenRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public TokenModel FindUserDataByToken(string refreshToken)
    {
        return _appDbContext.tokens.Where(t => t.token == refreshToken).FirstOrDefault();
    }

    public async Task<TokenModel> RegisterToken(TokenModel token)
    {
        await _appDbContext.tokens.AddAsync(token);
        await _appDbContext.SaveChangesAsync();
        return token;
    }

    public async Task UpdateToken(string email, string newRefreshToken)
    {
        await _appDbContext.Database.ExecuteSqlInterpolatedAsync(
            $"UPDATE tokens SET token = {newRefreshToken} WHERE email = {email}"
        );
        await _appDbContext.SaveChangesAsync();
    }
}