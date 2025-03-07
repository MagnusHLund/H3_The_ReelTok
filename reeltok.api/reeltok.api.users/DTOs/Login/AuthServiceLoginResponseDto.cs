namespace reeltok.api.users.DTOs.Login
{
    public class AuthServiceLoginResponseDto : BaseResponseDto
    {
        public AuthServiceLoginResponseDto(bool success) : base(success) { }
        public AuthServiceLoginResponseDto() { }
    }
}