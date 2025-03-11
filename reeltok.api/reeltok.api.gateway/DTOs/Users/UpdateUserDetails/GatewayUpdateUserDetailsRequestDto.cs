using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class GatewayUpdateUserDetailsRequestDto
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [Range(1, 320)]
        [EmailAddress]
        public string Email { get; set; }

        public GatewayUpdateUserDetailsRequestDto(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
