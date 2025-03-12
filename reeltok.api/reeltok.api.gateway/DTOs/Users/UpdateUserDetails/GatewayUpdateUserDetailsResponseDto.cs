using reeltok.api.gateway.Entities.Users;
using reeltok.api.gateway.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class GatewayUpdateUserDetailsResponseDto : BaseResponseDto
    {
        [Required]
        public UserEntity User { get; set; }

        public GatewayUpdateUserDetailsResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
