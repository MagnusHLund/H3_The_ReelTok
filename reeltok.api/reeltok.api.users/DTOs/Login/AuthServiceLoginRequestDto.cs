using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.Login
{
    public class AuthServiceLoginRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Password { get; set; }

        public AuthServiceLoginRequestDto(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        public AuthServiceLoginRequestDto() { }
    }
}