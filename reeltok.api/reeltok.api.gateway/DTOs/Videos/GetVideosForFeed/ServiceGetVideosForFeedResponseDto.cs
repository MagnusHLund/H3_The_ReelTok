using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Videos;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    public class ServiceGetVideosForFeedResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Videos")]
        public List<VideoForFeedEntity> Videos { get; set; }

        public ServiceGetVideosForFeedResponseDto(List<VideoForFeedEntity> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
