using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser
{
    public class ServiceGetAllSubscriptionsForUserResponseDto : BaseResponseDto
    {
        [Required]
        public List<UserDetails> Users { get; set; }

        public ServiceGetAllSubscriptionsForUserResponseDto(List<UserDetails> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}
