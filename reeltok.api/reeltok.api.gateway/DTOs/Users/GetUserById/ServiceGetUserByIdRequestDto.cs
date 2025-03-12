using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetUserProfileData
{
    public class ServiceGetUserByIdRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        public ServiceGetUserByIdRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}
