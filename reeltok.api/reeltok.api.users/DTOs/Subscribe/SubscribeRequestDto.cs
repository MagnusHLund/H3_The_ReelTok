using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.SubscriptionRequests
{
    public class SubscribeRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; }

        [Required]
        [JsonProperty("SubscribingToUserId")]
        public Guid SubscribingToUserId { get; }

        public SubscribeRequestDto(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }
    }
}
