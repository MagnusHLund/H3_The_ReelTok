using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser
{
    public class GatewayGetAllSubscriptionsForUserResponseDto : BaseResponseDto
    {
        [Required]
        public List<UserDetails> Users { get; set; }

        public GatewayGetAllSubscriptionsForUserResponseDto(List<UserDetails> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}
