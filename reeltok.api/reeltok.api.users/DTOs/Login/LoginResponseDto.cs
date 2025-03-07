using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs.Login
{
    public class LoginResponseDto : BaseResponseDto
    {
        public UserEntity User { get; set; }

        public LoginResponseDto(UserEntity user)
        {
            User = user;
        }

        public LoginResponseDto() { }
    }
}