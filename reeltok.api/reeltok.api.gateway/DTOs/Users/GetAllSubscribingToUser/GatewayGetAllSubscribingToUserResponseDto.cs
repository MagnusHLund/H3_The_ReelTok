using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser
{
    public class GatewayGetAllSubscribingToUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Users")]
        public List<ExternalUserEntity> Users { get; set; }

        public GatewayGetAllSubscribingToUserResponseDto(List<ExternalUserEntity> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}
