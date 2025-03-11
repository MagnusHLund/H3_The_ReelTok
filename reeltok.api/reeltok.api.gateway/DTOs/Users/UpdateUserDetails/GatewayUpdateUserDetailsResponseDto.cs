using reeltok.api.gateway.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class GatewayUpdateUserDetailsResponseDto : BaseResponseDto, IEditableUserDetailsDto
    {
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }

        public GatewayUpdateUserDetailsResponseDto(string username, string email, bool success = true) : base(success)
        {
            Username = username;
            Email = email;
        }

        public GatewayUpdateUserDetailsResponseDto() { }
    }
}
