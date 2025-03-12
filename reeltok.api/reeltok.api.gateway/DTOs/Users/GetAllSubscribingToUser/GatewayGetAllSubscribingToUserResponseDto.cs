using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser
{
    public class GatewayGetAllSubscribingToUserResponseDto : BaseResponseDto
    {
        [Required]
        public List<ExternalUserEntity> Users { get; set; }

        public GatewayGetAllSubscribingToUserResponseDto(List<ExternalUserEntity> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}
