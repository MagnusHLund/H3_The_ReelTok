using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Entities.Users;

namespace reeltok.api.gateway.DTOs.Users.UpdateProfilePicture
{
    public class ServiceUpdateProfilePictureResponseDto : BaseResponseDto
    {
        [Required]
        public UserEntity User { get; set; }

        public ServiceUpdateProfilePictureResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
