
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs
{
    internal class GetAllBlockedUsersRequestDto
    {
        [Required]
        internal Guid UserId { get; }

        internal GetAllBlockedUsersRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}