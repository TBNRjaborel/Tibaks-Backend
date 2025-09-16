// Services/JwtService.cs
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tibaks_Backend.Auth;
namespace Tibaks_Backend.Auth.Services{


public class JwtService
{
    private readonly IConfiguration _config;
    public JwtService(IConfiguration config) => _config = config;

    public string GenerateAccessToken(ApplicationUser user, IList<string>? roles = null)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        if (roles != null)
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:AccessTokenExpirationMinutes"]!)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(string ipAddress)
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            Expires = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenExpirationDays"]!)),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is JwtSecurityToken jwt &&
            jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return principal;
        }
        return null;
    }
}

}