public interface IJwtService {
    public string GenerateAccessToken(UserModel user);
    public string GenerateRefreshToken();
}