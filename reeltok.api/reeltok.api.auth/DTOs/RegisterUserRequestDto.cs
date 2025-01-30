
using System.ComponentModel.DataAnnotations;


namespace reeltok.api.auth.DTOs
{
    public class RegisterUserRequestDto
    {
        [Required]
        internal Guid UserId { get; private set; }
        [Required]
        internal string PlainTextPassword { get; private set; }

        internal RegisterUserRequestDto(Guid userId, string plainTextPassword)
        {
            UserId = userId;
            PlainTextPassword = plainTextPassword;
        }
    }
}