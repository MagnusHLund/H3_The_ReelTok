using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs.UpdateProfilePicture
{
    public class UpdateUserProfilePictureResponseDto : BaseResponseDto
    {
        public UserEntity User { get; set; }

        public UpdateUserProfilePictureResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }

        public UpdateUserProfilePictureResponseDto() { }
    }
}