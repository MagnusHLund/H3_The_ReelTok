using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser
{
    public class ServiceGetAllSubscribingToUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Subscribers")]
        public List<ExternalUserEntity> Users { get; set; }

        public ServiceGetAllSubscribingToUserResponseDto(List<ExternalUserEntity> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}
