using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using reeltok.api.users.Entities;

namespace reeltok.api.users.ValueObjects
{
    public class SubscribptionDetails
    {
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; private set; }
        public UserProfileData User { get; private set; }

        [Required]
        [ForeignKey("SubscribeToUser")]
        public Guid SubscribingToUserId { get; private set; }
        public UserProfileData SubscribeToUser { get; private set; }

        public SubscribptionDetails(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }

        private SubscribptionDetails() { }
    }
}