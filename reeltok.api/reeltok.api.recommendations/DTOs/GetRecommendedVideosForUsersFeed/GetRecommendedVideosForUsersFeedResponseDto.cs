using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs.GetRecommendedVideosForUsersFeed
{
    public class GetRecommendedVideosForUsersFeedResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("VideoIds")]
        public List<Guid> VideoIds { get; set; }

        public GetRecommendedVideosForUsersFeedResponseDto(List<Guid> videoIds, bool success = true) : base(success)
        {
            VideoIds = videoIds;
        }
    }
}