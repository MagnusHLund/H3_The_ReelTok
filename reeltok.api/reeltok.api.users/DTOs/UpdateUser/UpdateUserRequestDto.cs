using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.UpdateUser
{
    public class UpdateUserRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; }

        [JsonProperty("Username")]
        public string? Username { get; }
        
        [JsonProperty("Email")]
        public string? Email { get; }

        public UpdateUserRequestDto(Guid userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}
