using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.Login
{
    public class AuthServiceLoginRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Password")]
        public string Password { get; set; }

        public AuthServiceLoginRequestDto(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }
    }
}
