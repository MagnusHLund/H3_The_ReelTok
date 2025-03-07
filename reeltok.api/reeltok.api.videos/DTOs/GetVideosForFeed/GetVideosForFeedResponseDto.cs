using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetVideosForFeed
{
    public class GetVideosForFeedResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Videos")]
        public List<VideoForFeedEntity> Videos { get; set; }

        public GetVideosForFeedResponseDto(List<VideoForFeedEntity> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
