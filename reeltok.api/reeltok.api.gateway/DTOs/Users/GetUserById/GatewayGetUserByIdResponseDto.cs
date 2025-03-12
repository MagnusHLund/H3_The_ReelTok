using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Entities.Users;
using reeltok.api.gateway.Interfaces.DTOs;

namespace reeltok.api.gateway.DTOs.Users.GetUserProfileData
{
    public class GatewayGetUserByIdResponseDto : BaseResponseDto
    {
        [Required]
        public ExternalUserEntity User { get; set; }

        public GatewayGetUserByIdResponseDto(ExternalUserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
