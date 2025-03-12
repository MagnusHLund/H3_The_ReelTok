using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.UpdateProfilePicture
{
    public class ServiceUpdateProfilePictureRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("ProfilePicture")]
        public IFormFile ProfilePicture { get; set; }

        public ServiceUpdateProfilePictureRequestDto(Guid userId, IFormFile profilePicture)
        {
            UserId = userId;
            ProfilePicture = profilePicture;
        }

        // Parameterless constructor required for multipart/form-data requests
        public ServiceUpdateProfilePictureRequestDto() { }
    }
}
