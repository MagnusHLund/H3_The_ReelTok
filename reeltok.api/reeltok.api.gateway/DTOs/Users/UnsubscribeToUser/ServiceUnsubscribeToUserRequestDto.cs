using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.UnsubscribeToUser
{
    public class ServiceUnsubscribeToUserRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; }

        [Required]
        [JsonProperty("UnsubscribingToUserId")]
        public Guid UnsubscribingToUserId { get; }

        public ServiceUnsubscribeToUserRequestDto(Guid userId, Guid unsubscribingToUserId)
        {
            UserId = userId;
            UnsubscribingToUserId = unsubscribingToUserId;
        }
    }
}
