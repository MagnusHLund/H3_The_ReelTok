using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.Login
{
    public class LoginRequestDto
    {
        [Required]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("Password")]
        public string Password { get; set; }

        public LoginRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
