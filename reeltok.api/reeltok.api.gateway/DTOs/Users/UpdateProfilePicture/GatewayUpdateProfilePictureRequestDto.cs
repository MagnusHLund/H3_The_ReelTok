using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.UpdateProfilePicture
{
    public class GatewayUpdateProfilePictureRequestDto
    {
        [Required]
        [JsonProperty("ProfilePicture")]
        public IFormFile ProfilePicture { get; set; }

        public GatewayUpdateProfilePictureRequestDto(IFormFile profilePicture)
        {
            ProfilePicture = profilePicture;
        }
    }
}
