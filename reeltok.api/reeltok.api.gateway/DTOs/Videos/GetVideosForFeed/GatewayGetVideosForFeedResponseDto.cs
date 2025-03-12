using reeltok.api.gateway.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    public class GatewayGetVideosForFeedResponseDto : BaseResponseDto
    {
        [Required]
        public List<VideoForFeedEntity> Videos { get; set; }

        public GatewayGetVideosForFeedResponseDto(List<VideoForFeedEntity> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
