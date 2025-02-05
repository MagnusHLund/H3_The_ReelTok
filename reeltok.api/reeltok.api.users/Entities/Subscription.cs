using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class Subscription
    {
        [Required]
        internal Guid UserId { get; set; }
        
        [Required]
        internal SubscribptionDetails SubDetails { get; set; }

        public Subscription(Guid userId, SubscribptionDetails subDetails)
        {
            UserId = userId;
            SubDetails = subDetails;
        }
    }
}