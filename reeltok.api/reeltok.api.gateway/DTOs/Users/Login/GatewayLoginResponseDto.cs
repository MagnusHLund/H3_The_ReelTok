using reeltok.api.gateway.Entities.Users;
using reeltok.api.gateway.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.Login
{
    public class GatewayLoginResponseDto : BaseResponseDto
    {
        [Required]
        public UserEntity User { get; set; }

        public GatewayLoginResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
