using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateProfilePicture
{
    public class GatewayUpdateProfilePictureRequestDto
    {
        [Required]
        public IFormFile ProfilePicture { get; set; }

        public GatewayUpdateProfilePictureRequestDto(IFormFile profilePicture)
        {
            ProfilePicture = profilePicture;
        }
    }
}
