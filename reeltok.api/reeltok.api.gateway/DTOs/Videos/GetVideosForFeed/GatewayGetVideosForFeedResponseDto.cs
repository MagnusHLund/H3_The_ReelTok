using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Videos;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    public class GatewayGetVideosForFeedResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Videos")]
        public List<VideoForFeedUsingDateTimeEntity> Videos { get; set; }

        public GatewayGetVideosForFeedResponseDto(
            List<VideoForFeedUsingDateTimeEntity> videos,
            bool success = true
        ) : base(success)
        {
            Videos = videos;
        }
    }
}
