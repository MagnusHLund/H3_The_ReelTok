using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    public class ServiceGetVideosForFeedResponseDto : BaseResponseDto
    {
        [Required]
        public List<VideoForFeedEntity> Videos { get; set; }

        public ServiceGetVideosForFeedResponseDto(List<VideoForFeedEntity> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
