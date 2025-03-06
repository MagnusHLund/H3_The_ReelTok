using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.GetSubscriptions
{
    public class GetSubscriptionsResponseDto : BaseResponseDto
    {
        [Required]
        public List<ExternalUserEntity> Subscriptions { get; }

        public GetSubscriptionsResponseDto(List<ExternalUserEntity> subscriptions, bool success = true) : base(success)
        {
            Subscriptions = subscriptions;
        }
    }
}