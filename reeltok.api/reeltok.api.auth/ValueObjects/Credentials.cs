using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
  public class Credentials
  {
    [Required]
    public Guid UserId { get; private set; }

    [Required]
    public string PlainTextPassword { get; private set; }

    public Credentials(Guid userId, string plainTextPassword)
    {
      UserId = userId;
      PlainTextPassword = plainTextPassword;
    }
  }
}
