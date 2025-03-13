using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.SubscribeToUser
{
    public class GatewaySubscribeToUserRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid SubscribingToUserId { get; }

        public GatewaySubscribeToUserRequestDto(Guid subscribingToUserId)
        {
            SubscribingToUserId = subscribingToUserId;
        }
    }
}