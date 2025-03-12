using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.Login
{
    public class GatewayLoginRequestDto
    {
        [Required]
        [StringLength(320, MinimumLength = 3)]
        [EmailAddress]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8)]
        [JsonProperty("Password")]
        public string Password { get; set; }

        public GatewayLoginRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
