using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using reeltok.api.users.Entities;

namespace reeltok.api.users.ValueObjects
{
    public class SubscribptionDetails
    {
        [Required]
        public Guid SubscriberUserId { get; private set; }

        [Required]
        public Guid SubscribingToUserId { get; private set; }

        public SubscribptionDetails(Guid subscriberUserId, Guid subscribingToUserId)
        {
            SubscriberUserId = subscriberUserId;
            SubscribingToUserId = subscribingToUserId;
        }
        private SubscribptionDetails() { }
    }
}