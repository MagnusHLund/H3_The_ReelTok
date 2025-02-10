using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
  public class AccessToken
  {
    [Required]
    public string Token { get; private set; }

    [Required]
    public DateTime CreateDate { get; private set; }

    [Required]
    public DateTime ExpireTime { get; private set; }


    public AccessToken(string token, DateTime createDate, DateTime expireTime)
    {
      Token = token;
      CreateDate = createDate;
      ExpireTime = expireTime;
    }
  }
} 
