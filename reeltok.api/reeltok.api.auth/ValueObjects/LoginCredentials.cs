using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
  public class LoginCredentials
  {
    [Required]
    public Guid UserId { get; private set; }

    [Required]
    public string PlainTextPassword { get; private set; }

    public LoginCredentials(Guid userId, string plainTextPassword)
    {
       UserId = userId;
       PlainTextPassword = plainTextPassword;
    }
  }
}
