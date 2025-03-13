using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.UnsubscribeToUser
{
    public class ServiceUnsubscribeToUserRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; }

        [Required]
        [JsonProperty("SubscribingToUserId")]
        public Guid SubscribingToUserId { get; }

        public ServiceUnsubscribeToUserRequestDto(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }
    }
}
