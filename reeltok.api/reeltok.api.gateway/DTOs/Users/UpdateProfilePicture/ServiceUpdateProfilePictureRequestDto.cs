using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateProfilePicture
{
    public class ServiceUpdateProfilePictureRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public IFormFile ProfilePicture { get; set; }

        public ServiceUpdateProfilePictureRequestDto(Guid userId, IFormFile profilePicture)
        {
            UserId = userId;
            ProfilePicture = profilePicture;
        }
    }
}
