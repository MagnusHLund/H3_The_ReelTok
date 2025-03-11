using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateProfilePicture
{
    public class ServiceUpdateProfilePictureResponseDto : BaseResponseDto
    {
        [Required]
        [StringLength(50)]
        public string ProfilePictureUrl { get; set; }

        public ServiceUpdateProfilePictureResponseDto(string profilePictureUrl, bool success = true) : base(success)
        {
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
