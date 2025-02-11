using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs.SubscriptionRequests
{
    public class SubscribeRequestDto
    {
        [Required]
        public Guid SubscriberUserId { get; }

        [Required]
        public Guid SubscribingToUserId { get; }

        public SubscribeRequestDto(Guid subscriberUserId, Guid subscribingToUserId)
        {
            SubscriberUserId = subscriberUserId;
            SubscribingToUserId = subscribingToUserId;
        }
    }
}