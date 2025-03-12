using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser
{
    public class GatewayGetAllSubscriptionsForUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Users")]
        public List<ExternalUserEntity> Users { get; set; }

        public GatewayGetAllSubscriptionsForUserResponseDto(List<ExternalUserEntity> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}
