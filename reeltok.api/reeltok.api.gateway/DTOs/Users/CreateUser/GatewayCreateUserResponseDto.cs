using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.CreateUser
{
    public class GatewayCreateUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public UserWithInterestEntity User { get; set; }

        public GatewayCreateUserResponseDto(UserWithInterestEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
