using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.Login
{
    public class ServiceLoginResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public UserWithInterestEntity User { get; set; }

        public ServiceLoginResponseDto(UserWithInterestEntity user)
        {
            User = user;
        }
    }
}
