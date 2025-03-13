using Newtonsoft.Json;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class UserWithSubscriptionCounts : ExternalUserEntity
    {
        [JsonProperty("TotalSubscribers")]
        public int TotalSubscribers { get; set; }

        [JsonProperty("TotalSubscriptions")]
        public int TotalSubscriptions { get; set; }

        public UserWithSubscriptionCounts(
            Guid userId,
            UserDetails userDetails,
            int totalSubscribers,
            int totalSubscriptions
        ) : base(userId, userDetails)
        {
            TotalSubscribers = totalSubscribers;
            TotalSubscriptions = totalSubscriptions;
        }
    }
}
