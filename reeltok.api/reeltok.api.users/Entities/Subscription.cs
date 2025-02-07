using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class Subscription
    {
        [Key]
        public uint SubscriptionId { get; set; }

        [Required]
        public SubscribptionDetails SubDetails { get; set; }

        public Subscription(uint subscriptionId, SubscribptionDetails subDetails)
        {
            SubscriptionId = subscriptionId;
            SubDetails = subDetails;
        }
        private Subscription() { }
    }
}