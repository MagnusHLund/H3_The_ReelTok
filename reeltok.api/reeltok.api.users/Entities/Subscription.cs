using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class Subscribption
    {
        [Required]
        internal Guid UserId { get; private set; }
        
        [Required]
        internal SubscribptionDetails SubDetails { get; private set; }

        public Subscribption(Guid userId, SubscribptionDetails subDetails)
        {
            UserId = userId;
            SubDetails = subDetails;
        }
    }
}