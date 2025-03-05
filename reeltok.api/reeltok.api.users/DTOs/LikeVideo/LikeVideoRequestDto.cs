using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.LikeVideo
{
    public class LikeVideoRequestDto
    {
        [Required]
        public Guid UserId { get; }

        [Required]
        public Guid VideoId { get; }

        public LikeVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}