using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class GatewayUpdateUserDetailsResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public UserEntity User { get; set; }

        public GatewayUpdateUserDetailsResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }

        public GatewayUpdateUserDetailsResponseDto() { }
    }
}
