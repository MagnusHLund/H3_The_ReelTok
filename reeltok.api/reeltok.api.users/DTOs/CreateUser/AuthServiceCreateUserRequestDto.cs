using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.CreateUser
{
    public class AuthServiceCreateUserRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Password { get; set; }

        public AuthServiceCreateUserRequestDto(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }
    }
}