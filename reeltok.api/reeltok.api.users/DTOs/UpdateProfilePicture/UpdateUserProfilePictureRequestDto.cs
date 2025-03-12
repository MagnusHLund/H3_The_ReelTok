using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.UpdateProfilePicture
{
    public class UpdateUserProfilePictureRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("ProfilePicture")]
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
