using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.SubscriptionRequests
{
    public class SubscribeRequestDto
    {
        [Required]
        public Guid UserId { get; }

        [Required]
        public Guid SubscribingToUserId { get; }

        public SubscribeRequestDto(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }
    }
}