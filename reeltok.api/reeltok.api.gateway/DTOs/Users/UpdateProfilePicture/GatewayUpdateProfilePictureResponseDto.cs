using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Entities.Users;

namespace reeltok.api.gateway.DTOs.Users.UpdateProfilePicture
{
    public class GatewayUpdateProfilePictureResponseDto : BaseResponseDto
    {
        [Required]
        public UserEntity User { get; set; }

        public GatewayUpdateProfilePictureResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
