using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Entites
{
    public class Auth
    {
      [Required]
      public Guid UserId { get; private set; }
     
      [Required]
      public string HashedPassword { get; private set; }

      [Required] 
      public string Salt { get; private set; }
     
      public Auth(Guid userId, string hashedPassword, string salt)
      {
        UserId = userId;
        HashedPassword = hashedPassword;
        Salt = salt;
      }
    }
}
