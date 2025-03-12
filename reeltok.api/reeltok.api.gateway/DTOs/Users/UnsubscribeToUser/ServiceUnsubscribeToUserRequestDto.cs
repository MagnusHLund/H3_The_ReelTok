using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UnsubscribeToUser
{
    public class ServiceUnsubscribeToUserRequestDto
    {
        [Required]
        public Guid UserId { get; }

        [Required]
        public Guid SubscribingToUserId { get; }

        public ServiceUnsubscribeToUserRequestDto(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }
    }
}
