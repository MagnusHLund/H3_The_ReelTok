using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser
{
    public class ServiceGetAllSubscribingToUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Subscriptions")]
        public List<ExternalUserEntity> Users { get; set; }

        public ServiceGetAllSubscribingToUserResponseDto(List<ExternalUserEntity> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}
