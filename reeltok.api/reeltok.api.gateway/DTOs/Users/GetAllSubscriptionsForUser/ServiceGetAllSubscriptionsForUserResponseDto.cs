using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser
{
    public class ServiceGetAllSubscriptionsForUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Subscriptions")]
        public List<ExternalUserEntity> Users { get; set; }

        public ServiceGetAllSubscriptionsForUserResponseDto(List<ExternalUserEntity> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}
