using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.CreateUser
{
    public class AuthServiceCreateUserRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Password")]
        public string Password { get; set; }

        public AuthServiceCreateUserRequestDto(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }
    }
}
