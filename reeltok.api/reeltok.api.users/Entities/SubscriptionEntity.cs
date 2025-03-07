using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.Entities
{
    public class SubscriptionEntity
    {
        [Key]
        public uint SubscriptionId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid SubscribingToUserId { get; set; }

        public SubscriptionEntity(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }

        private SubscriptionEntity() { }
    }
}