using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.SubscribeToUser
{
    public class ServiceSubscribeToUserRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; }

        [Required]
        [JsonProperty("SubscribingToUserId")]
        public Guid SubscribingToUserId { get; }

        public ServiceSubscribeToUserRequestDto(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }
    }
}
