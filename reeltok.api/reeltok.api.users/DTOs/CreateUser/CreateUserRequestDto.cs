using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.CreateUser
{
    public class CreateUserRequestDto
    {
        [Required]
        public string Username { get; }
        [Required]
        public string Email { get; }
        [Required]
        public string Password { get; }

        public CreateUserRequestDto(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
