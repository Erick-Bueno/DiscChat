using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneOf;

public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;
    public JwtService(IConfiguration configuration, ITokenRepository tokenRepository, IUserRepository userRepository)
    {
        this.configuration = configuration;
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
    }

    public string GenerateAccessToken(UserEntity user)
    {
        var generateToken = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("KeyAccessToken"));
        var contentToken = new SecurityTokenDescriptor();
        var claims = new Claim[]{
            new Claim(ClaimTypes.NameIdentifier, user.externalId.ToString()),
            new Claim(ClaimTypes.Name, user.name)
        };
        var claim = new ClaimsIdentity(claims);
        contentToken.Subject = new ClaimsIdentity(claim);
        contentToken.Expires = DateTime.UtcNow.AddHours(1);
        contentToken.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var token = generateToken.CreateToken(contentToken);
        return generateToken.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
            var generateToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("KeyRefreshToken"));
            var contentToken = new SecurityTokenDescriptor();
            contentToken.Expires = DateTime.UtcNow.AddHours(2);
            contentToken.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var token = generateToken.CreateToken(contentToken);
            return generateToken.WriteToken(token);


    }

    public async Task<OneOf<ResponseNewTokens, AppError>> RefreshToken(string refreshToken)
    {
        try
        {
            var tokenIsValid = ValidateToken(refreshToken);
            if (tokenIsValid == false)
            {
                return new InvalidRefreshToken("Refresh token invalido");
            }
            var userData = _tokenRepository.FindUserDataByToken(refreshToken);

            var userEntity = _userRepository.FindUserByEmail(userData.email);

            var newRefreshToken = GenerateRefreshToken();
            var newAccessToken = GenerateAccessToken(userEntity);

            await _tokenRepository.UpdateToken(userEntity.email, newRefreshToken);

            return new ResponseNewTokens(200, "Tokens atualizados com sucesso", newRefreshToken, newAccessToken);
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }


    }

    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("KeyRefreshToken"));
            var tokenValidation = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            return validatedToken != null;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
}