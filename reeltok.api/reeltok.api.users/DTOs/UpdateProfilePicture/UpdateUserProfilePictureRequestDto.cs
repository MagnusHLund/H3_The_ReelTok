using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.UpdateProfilePicture
{
    public class UpdateUserProfilePictureRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public IFormFile ProfilePicture { get; set; }

        public UpdateUserProfilePictureRequestDto(Guid userId, IFormFile profilePicture)
        {
            UserId = userId;
            ProfilePicture = profilePicture;
        }

        // Parameterless constructor required for multipart/form-data requests
        public UpdateUserProfilePictureRequestDto() { }
    }
}