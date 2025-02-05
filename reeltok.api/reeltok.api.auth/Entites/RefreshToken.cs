using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Entites
{
  public class RefreshToken 
  {
     [Required]
     public Guid UserId { get; set; }

     [Required]
     public string Token { get; set; }

     [Required]
     public DateTime CreateDate { get; set; }

     [Required]
     public DateTime ExpireDate { get; set; }

     
     public RefreshToken(Guid userId, string token, DateTime createDate, DateTime expireDate)
     {
       UserId = userId;
       Token = token;
       CreateDate = createDate;
       ExpireDate = expireDate;
     }

  }
}
