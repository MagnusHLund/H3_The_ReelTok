using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class SubscriptionEntity
    {
        [Key]
        public uint SubscriptionId { get; set; }

        [Required]
        public SubscriptionDetails SubDetails { get; set; }

        public SubscriptionEntity(SubscriptionDetails subDetails)
        {
            SubDetails = subDetails;
        }
        private SubscriptionEntity() { }
    }
}