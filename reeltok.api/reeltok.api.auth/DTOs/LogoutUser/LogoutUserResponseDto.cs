namespace reeltok.api.auth.DTOs
{
    public class LogoutUserResponseDto : BaseResponseDto
    {
        public LogoutUserResponseDto(bool success = true) : base(success) { }
        public LogoutUserResponseDto() { }
    }
}
