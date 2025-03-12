using Newtonsoft.Json;
using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.GetSubscriptions
{
    public class GetSubscriptionsResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Subscriptions")]
        public List<ExternalUserEntity> Subscriptions { get; }

        public GetSubscriptionsResponseDto(List<ExternalUserEntity> subscriptions, bool success = true) : base(success)
        {
            Subscriptions = subscriptions;
        }
    }
}
