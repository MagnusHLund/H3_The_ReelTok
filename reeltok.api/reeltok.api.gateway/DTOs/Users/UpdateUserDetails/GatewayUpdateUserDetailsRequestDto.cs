using Newtonsoft.Json;
using reeltok.api.gateway.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class GatewayUpdateUserDetailsRequestDto
    {
        [StringLength(25, MinimumLength = 3)]
        [JsonProperty("Username")]
        public string? Username { get; set; }

        [StringLength(320, MinimumLength = 3)]
        [EmailAddress]
        [JsonProperty("Email")]
        public string? Email { get; set; }

        [JsonProperty("Interest")]
        public CategoryType Interest { get; set; }

        public GatewayUpdateUserDetailsRequestDto(string? username, string? email, CategoryType interest)
        {
            Username = username;
            Email = email;
            Interest = interest;
        }
    }
}
