using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
  public class RegisterDetails
  {
    [Required]
    public Guid UserId { get; private set; }
    
    [Required]
    public string PlainTextPassword { get; private set; }

    public RegisterDetails(Guid userId, string plainTextPassword)
    {
       UserId = userId;
       PlainTextPassword = plainTextPassword;
    }    
  }    
}
