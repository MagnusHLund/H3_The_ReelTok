using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Videos;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    public class GatewayGetVideosForFeedResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Videos")]
        public List<VideoForFeedEntity> Videos { get; set; }

        public GatewayGetVideosForFeedResponseDto(List<VideoForFeedEntity> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
