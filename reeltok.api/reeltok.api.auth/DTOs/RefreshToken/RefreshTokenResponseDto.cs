namespace reeltok.api.auth.DTOs
{
    public class RefreshTokenResponseDto : BaseResponseDto
    {
        public RefreshTokenResponseDto(bool success = true) : base(success) { }
        public RefreshTokenResponseDto() { }
    }
}
