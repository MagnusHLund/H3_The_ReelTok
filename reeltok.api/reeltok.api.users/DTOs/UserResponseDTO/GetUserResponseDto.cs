using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.DTOs.UserResponseDTO
{
    internal class GetUserResponseDto : BaseResponseDto
    {
        internal string Username { get; }
        internal UserDetails UserDetails { get; }
        internal GetUserResponseDto(bool success, string userName, UserDetails userDetails) : base(success)
        {
            Username = userName;
            UserDetails = userDetails;
        }
    }
}