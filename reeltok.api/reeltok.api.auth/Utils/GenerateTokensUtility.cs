using reeltok.api.auth.Entites;
using reeltok.api.auth.ValueObjects;
using System.Security.Cryptography;

namespace reeltok.api.auth.Utils 
{
  public static class GenerateTokenUtility 
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
    
        var tokenString = Convert.ToBase64String(randomBytes);
        var createDate = DateTime.UtcNow;
        var expireDate = createDate.AddDays(7);

        return new RefreshToken(userId, tokenString, createDate, expireDate);
    }

    public static AccessToken GenerateAccessToken()
    {
      throw new NotImplementedException();
    }
  }  
}
