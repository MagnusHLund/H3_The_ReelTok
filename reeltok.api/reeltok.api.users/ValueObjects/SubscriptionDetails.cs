using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.ValueObjects
{
    public class SubscriptionDetails
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; private set; }

        [Required]
        [JsonProperty("SubscribingToUserId")]
        public Guid SubscribingToUserId { get; private set; }

        public SubscriptionDetails(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }
        private SubscriptionDetails() { }
    }
}