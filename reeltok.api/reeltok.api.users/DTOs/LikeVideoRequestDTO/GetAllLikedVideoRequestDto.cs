
using System.ComponentModel.DataAnnotations;


namespace reeltok.api.users.DTOs.LikeVideoRequestDTO
{
    internal class GetAllLikedVideoRequestDto
    {
        [Required]
        internal Guid UserId { get; }

        internal GetAllLikedVideoRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}