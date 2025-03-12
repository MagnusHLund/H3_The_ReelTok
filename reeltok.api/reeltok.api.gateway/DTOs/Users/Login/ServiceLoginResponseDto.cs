using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.Login
{
    public class ServiceLoginResponseDto : BaseResponseDto
    {
        [Required]
        public UserEntity User { get; set; }

        public ServiceLoginResponseDto(UserEntity user)
        {
            User = user;
        }
    }
}
