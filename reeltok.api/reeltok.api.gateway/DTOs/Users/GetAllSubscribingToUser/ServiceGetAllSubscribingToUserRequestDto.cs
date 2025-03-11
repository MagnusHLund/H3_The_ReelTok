using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser
{
    public class ServiceGetAllSubscribingToUserRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        public ServiceGetAllSubscribingToUserRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}
