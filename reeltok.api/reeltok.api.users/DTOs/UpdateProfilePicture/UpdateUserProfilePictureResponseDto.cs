using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.UpdateProfilePicture
{
    public class UpdateUserProfilePictureResponseDto : BaseResponseDto
    {
        [Required]
        public UserEntity User { get; set; }

        public UpdateUserProfilePictureResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}