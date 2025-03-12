using Newtonsoft.Json;
using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.GetSubscribers
{
    public class GetSubscribersResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Subscriptions")]
        public List<ExternalUserEntity> Subscriptions { get; }

        public GetSubscribersResponseDto(List<ExternalUserEntity> subscriptions, bool success = true) : base(success)
        {
            Subscriptions = subscriptions;
        }
    }
}
