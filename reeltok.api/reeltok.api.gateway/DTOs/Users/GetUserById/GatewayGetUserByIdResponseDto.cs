using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;

namespace reeltok.api.gateway.DTOs.Users.GetUserProfileData
{
    public class GatewayGetUserByIdResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public ExternalUserEntity User { get; set; }

        public GatewayGetUserByIdResponseDto(ExternalUserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
