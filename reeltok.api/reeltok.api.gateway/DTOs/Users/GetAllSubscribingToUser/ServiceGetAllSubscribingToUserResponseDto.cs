using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser
{
    public class ServiceGetAllSubscribingToUserResponseDto : BaseResponseDto
    {
        [Required]
        public List<UserDetails> Users { get; set; }

        public ServiceGetAllSubscribingToUserResponseDto(List<UserDetails> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}
