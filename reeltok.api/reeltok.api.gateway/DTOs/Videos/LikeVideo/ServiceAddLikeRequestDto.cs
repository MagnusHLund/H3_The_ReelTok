using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.LikeVideo
{
    public class ServiceAddLikeRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid VideoId { get; set; }

        public ServiceAddLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
