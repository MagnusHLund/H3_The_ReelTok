namespace reeltok.api.auth.ValueObjects
{
  public class Tokens
  {
    public AccessToken AccessToken { get; private set; }

    public RefreshToken RefreshToken { get; private set; }

    public Tokens(AccessToken accessToken, RefreshToken refreshToken)
    {
      AccessToken = accessToken;
      RefreshToken = refreshToken;
    }
  }
}
