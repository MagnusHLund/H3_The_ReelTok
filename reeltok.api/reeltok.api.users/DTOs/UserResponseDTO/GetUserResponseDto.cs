using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.DTOs.UserResponseDTO
{
    public class GetUserResponseDto : BaseResponseDto
    {
        public string Username { get; }
        public UserDetails UserDetails { get; }
        public GetUserResponseDto(bool success, string userName, UserDetails userDetails) : base(success)
        {
            Username = userName;
            UserDetails = userDetails;
        }
    }
}