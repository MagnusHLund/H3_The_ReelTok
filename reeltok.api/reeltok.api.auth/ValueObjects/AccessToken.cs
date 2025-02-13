using System.ComponentModel.DataAnnotations;
using reeltok.api.auth.Interfaces;

namespace reeltok.api.auth.ValueObjects
{
  public class AccessToken : IToken
  {
    [Required]
    public string Token { get; private set; }

    [Required]
    public DateTime CreateDate { get; private set; }

    [Required]
    public DateTime ExpireDate { get; private set; }


    public AccessToken(string token, DateTime createDate, DateTime expireDate)
    {
        Token = token;
        CreateDate = createDate;
        ExpireDate = expireDate;
    }
  }
}
