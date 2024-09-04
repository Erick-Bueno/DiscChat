using OneOf;

public interface IJwtService {
    public string GenerateAccessToken(UserEntity user);
    public string GenerateRefreshToken();
    public bool ValidateToken(string token);
    public Task<OneOf<ResponseNewTokens, AppError>> RefreshToken(string refreshToken);
}