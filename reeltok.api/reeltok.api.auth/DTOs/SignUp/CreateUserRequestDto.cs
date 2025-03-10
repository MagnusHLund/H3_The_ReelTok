using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs.SignUp
{
    public class SignUpRequestDto
    {
        [Required]
        [JsonProperty("UserId")]

        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Password")]
        public string PlainTextPassword { get; set; }

        public SignUpRequestDto(Guid userId, string plainTextPassword)
        {
            UserId = userId;
            PlainTextPassword = plainTextPassword;
        }
    }
}
