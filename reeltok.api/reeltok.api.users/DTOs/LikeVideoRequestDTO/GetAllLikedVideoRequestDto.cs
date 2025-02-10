
using System.ComponentModel.DataAnnotations;


namespace reeltok.api.users.DTOs.LikeVideoRequestDTO
{
    public class GetAllLikedVideoRequestDto
    {
        [Required]
        public Guid UserId { get; }

        public GetAllLikedVideoRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}