using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Auth.GetUserIdByToken
{
    public class ServiceGetUserIdByTokenResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        public ServiceGetUserIdByTokenResponseDto(Guid userId, bool success = true) : base(success)
        {
            UserId = userId;
        }

    }
}
