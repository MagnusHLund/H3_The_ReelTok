using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs
{
    public class LoginUserRequestDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string PlainTextPassword { get; set; }

        public LoginUserRequestDto(Guid userId, string plainTextPassword)
        {
            UserId = userId;
            PlainTextPassword = plainTextPassword;
        }

        public LoginUserRequestDto() { }
    }
}
