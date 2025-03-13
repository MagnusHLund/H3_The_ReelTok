using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.GetUserProfileData
{
    public class ServiceGetUserByIdRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        public ServiceGetUserByIdRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}
