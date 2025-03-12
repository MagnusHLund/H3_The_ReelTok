using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.CreateUser
{
    public class CreateUserRequestDto
    {
        [Required]
        [JsonProperty("Username")]
        public string Username { get; }
        [Required]
        [JsonProperty("Email")]
        public string Email { get; }
        [Required]
        [JsonProperty("Password")]
        public string Password { get; }
        [Required]
        [JsonProperty("Interests")]
        public byte Interests { get; }

        public CreateUserRequestDto(string username, string email, string password, byte interests)
        {
            Username = username;
            Email = email;
            Password = password;
            Interests = interests;
        }
    }
}
