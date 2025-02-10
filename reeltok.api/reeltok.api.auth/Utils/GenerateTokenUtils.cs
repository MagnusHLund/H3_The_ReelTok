using reeltok.api.auth.Entites;
using reeltok.api.auth.ValueObjects;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace reeltok.api.auth.Utils
{
  public static class GenerateTokenUtils
  {
    /// <summary>
    /// Generates a secure random refresh token.
    /// </summary>
    /// <param name="size">The size of the token in bytes. Default is 32 bytes.</param>
    /// <returns>A Base64-encoded refresh token string.</returns>
    public static RefreshToken GenerateRefreshToken(Guid userId)
    {
        var randomBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        string tokenString = Convert.ToBase64String(randomBytes);
        DateTime createDate = DateTime.UtcNow;
        DateTime expireDate = createDate.AddDays(7);

        return new RefreshToken(userId, tokenString, createDate, expireDate);
    }


    public static AccessToken GenerateAccessToken(Guid userId, string secretKey, string issuer, string audience)
    {
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT SecretKey is not provided.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        DateTime createDate = DateTime.UtcNow;
        DateTime expireDate = createDate.AddHours(1);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expireDate,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AccessToken(tokenHandler.WriteToken(token), createDate, expireDate);
    }
  }
}
