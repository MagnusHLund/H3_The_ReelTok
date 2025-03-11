using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.DeleteVideo
{
    public class ServiceDeleteVideoRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid VideoId { get; set; }

        public ServiceDeleteVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
