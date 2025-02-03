using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.Entities;

namespace reeltok.api.users.ValueObjects
{
    public class SubscribptionDetails
    {
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; } = Guid.Empty;
        public UserProfileData? User { get; } = null;

        [Required]
        [ForeignKey("SubscribeToUser")]
        public Guid SubscriptionId { get; } = Guid.Empty;
        public UserProfileData? SubscribeToUser { get; } = null;

        public SubscribptionDetails(Guid userId, Guid subscriptionId)
        {
            UserId = userId;
            SubscriptionId = subscriptionId;
        }
    }
}