using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.RemoveLike
{
    public class ServiceRemoveLikeRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid VideoId { get; set; }

        public ServiceRemoveLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
