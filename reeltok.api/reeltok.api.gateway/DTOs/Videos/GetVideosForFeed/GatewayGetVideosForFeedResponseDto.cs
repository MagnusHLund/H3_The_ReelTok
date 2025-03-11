using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    public class GatewayGetVideosForFeedResponseDto : BaseResponseDto
    {
        [Required]
        public List<Video> Videos { get; set; }

        public GatewayGetVideosForFeedResponseDto(List<Video> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
