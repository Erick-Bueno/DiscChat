using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;

    public JwtService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GenerateAccessToken(UserModel user)
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
}