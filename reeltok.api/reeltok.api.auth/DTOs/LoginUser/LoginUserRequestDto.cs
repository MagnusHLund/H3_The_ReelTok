
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs
{
    public class LoginUserRequestDto
    {
        [Required]
        public Guid UserId { get; private set; }
        [Required]
        public string PlainTextPassword { get; private set; }

        public LoginUserRequestDto(Guid userId, string plainTextPassword)
        {
            UserId = userId;
            PlainTextPassword = plainTextPassword;
        }
    }
}
