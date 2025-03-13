using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.CreateUser
{
    public class GatewayCreateUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public UserEntity User { get; set; }

        public GatewayCreateUserResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
