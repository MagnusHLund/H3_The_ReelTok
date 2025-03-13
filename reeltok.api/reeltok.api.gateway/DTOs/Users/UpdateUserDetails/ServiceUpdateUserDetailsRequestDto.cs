using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class ServiceUpdateUserDetailsRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [JsonProperty("Username")]
        public string? Username { get; set; }

        [StringLength(320, MinimumLength = 3)]
        [EmailAddress]
        [JsonProperty("Email")]
        public string? Email { get; set; }

        [JsonProperty("Interest")]
        public CategoryType? Interest { get; set; }

        public ServiceUpdateUserDetailsRequestDto(Guid userId, string? username, string? email, CategoryType? interest)
        {
            UserId = userId;
            Username = username;
            Email = email;
            Interest = interest;
        }
    }
}
