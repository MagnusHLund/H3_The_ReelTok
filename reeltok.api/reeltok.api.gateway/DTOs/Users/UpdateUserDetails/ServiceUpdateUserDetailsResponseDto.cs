using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class ServiceUpdateUserDetailsResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public UserEntity User { get; set; }

        public ServiceUpdateUserDetailsResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
