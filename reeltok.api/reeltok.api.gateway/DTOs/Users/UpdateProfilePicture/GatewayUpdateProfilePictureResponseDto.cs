using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;

namespace reeltok.api.gateway.DTOs.Users.UpdateProfilePicture
{
    public class GatewayUpdateProfilePictureResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public UserEntity User { get; set; }

        public GatewayUpdateProfilePictureResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
