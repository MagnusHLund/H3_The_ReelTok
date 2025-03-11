using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Auth.GetUserIdByToken
{
    public class ServiceGetUserIdByTokenResponseDto : BaseResponseDto
    {
        [Required]
        public Guid UserId { get; set; }

        public ServiceGetUserIdByTokenResponseDto(Guid userId, bool success = true) : base(success)
        {
            UserId = userId;
        }

    }
}
