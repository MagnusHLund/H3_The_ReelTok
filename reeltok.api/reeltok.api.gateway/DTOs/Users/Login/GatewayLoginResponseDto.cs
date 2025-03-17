using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.Login
{
    public class GatewayLoginResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public UserWithInterestEntity User { get; set; }

        public GatewayLoginResponseDto(UserWithInterestEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
