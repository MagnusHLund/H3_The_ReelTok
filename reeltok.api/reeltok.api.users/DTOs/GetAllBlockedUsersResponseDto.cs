using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.DTOs
{
    internal class GetAllBlockedUsersResponseDto : BaseResponseDto
    {
        [Required]
        internal List<BlockedUserDetail> BlockedUserDetails { get; private set; }

        internal GetAllBlockedUsersResponseDto(bool success, List<BlockedUserDetail> blockedUserDetails) : base(success)
        {
            BlockedUserDetails = blockedUserDetails;
        }
    }
}