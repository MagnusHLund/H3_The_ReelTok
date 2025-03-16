using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Videos;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    public class ServiceGetVideosForFeedResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Videos")]
        public List<VideoForFeedUsingUnixTimeEntity> Videos { get; set; }

        public ServiceGetVideosForFeedResponseDto(
            List<VideoForFeedUsingUnixTimeEntity> videos,
            bool success = true
        ) : base(success)
        {
            Videos = videos;
        }
    }
}
