using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.SubscribeToUser
{
    public class ServiceSubscribeToUserRequestDto
    {
        [Required]
        public Guid UserId { get; }

        [Required]
        public Guid SubscribingToUserId { get; }

        public ServiceSubscribeToUserRequestDto(Guid userId, Guid subscribingToUserId)
        {
            UserId = userId;
            SubscribingToUserId = subscribingToUserId;
        }
    }
}
