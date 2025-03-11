using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser
{
    public class ServiceGetAllSubscriptionsForUserRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        public ServiceGetAllSubscriptionsForUserRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}
