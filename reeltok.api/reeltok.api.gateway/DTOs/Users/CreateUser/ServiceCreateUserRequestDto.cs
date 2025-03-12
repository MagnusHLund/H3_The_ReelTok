using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.DTOs.Users.CreateUser
{
    public class ServiceCreateUserRequestDto
    {
        [Required]
        [EmailAddress]
        [StringLength(320, MinimumLength = 3)]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        [JsonProperty("Username")]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [JsonProperty("Password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("Interests")]
        public CategoryType Interests { get; set; }

        public ServiceCreateUserRequestDto(string email, string username, string password, CategoryType interests)
        {
            Email = email;
            Username = username;
            Password = password;
            Interests = interests;
        }
    }
}
