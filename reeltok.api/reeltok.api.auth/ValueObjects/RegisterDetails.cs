using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
  public class CreateDetails
  {
    [Required]
    public Guid UserId { get; private set; }

    [Required]
    public string PlainTextPassword { get; private set; }

    public CreateDetails(Guid userId, string plainTextPassword)
    {
       UserId = userId;
       PlainTextPassword = plainTextPassword;
    }
  }
}
