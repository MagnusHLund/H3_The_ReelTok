using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.Login
{
    public class LoginResponseDto : BaseResponseDto
    {
        [Required]
        public UserEntity User { get; set; }

        public LoginResponseDto(UserEntity user)
        {
            User = user;
        }
    }
}