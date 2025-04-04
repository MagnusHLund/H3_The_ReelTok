using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs.LoginUser
{
    public class LoginUserRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Password")]
        public string PlainTextPassword { get; set; }

        public LoginUserRequestDto(Guid userId, string plainTextPassword)
        {
            UserId = userId;
            PlainTextPassword = plainTextPassword;
        }
    }
}
