using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetUserProfileData
{
    public class ServiceGetUserProfileDataRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        public ServiceGetUserProfileDataRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}
