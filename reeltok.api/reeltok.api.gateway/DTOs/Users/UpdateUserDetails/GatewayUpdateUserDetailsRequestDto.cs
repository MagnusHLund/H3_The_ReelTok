using reeltok.api.gateway.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class GatewayUpdateUserDetailsRequestDto
    {
        [StringLength(25, MinimumLength = 3)]
        public string? Username { get; set; }

        [Range(1, 320)]
        [EmailAddress]
        public string? Email { get; set; }

        public CategoryType Interest { get; set; }

        public GatewayUpdateUserDetailsRequestDto(string? username, string? email, CategoryType interest)
        {
            Username = username;
            Email = email;
            Interest = interest;
        }
    }
}
