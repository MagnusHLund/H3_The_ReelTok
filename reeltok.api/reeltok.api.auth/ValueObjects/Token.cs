using System.ComponentModel.DataAnnotations;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Entites;

namespace reeltok.api.auth.ValueObjects
{
  public class Tokens
  {
    [Required]
    public AccessToken AccessToken { get; private set; }

    [Required]
    public RefreshToken RefreshToken { get; private set; }

    public Tokens(AccessToken accessToken, RefreshToken refreshToken)
    {
      AccessToken = accessToken;
      RefreshToken = refreshToken;
    }
  }
}
