using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.ValueObjects
{
    public class SubscriptionDetails
    {
        [Required]
        public Guid UserId { get; private set; }

        [Required]
        public Guid SubscribingToUserId { get; private set; }

        public SubscriptionDetails(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }
        private SubscriptionDetails() { }
    }
}