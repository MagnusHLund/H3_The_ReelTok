using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.videos.DTOs.GetRecommendedVideos
{
    public class RecommendedServiceGetRecommendedVideosResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("VideoIds")]
        public List<Guid> VideoIdList { get; set; }

        public RecommendedServiceGetRecommendedVideosResponseDto(List<Guid> videoIdList, bool success = true) : base(success)
        {
            VideoIdList = videoIdList;
        }
    }
}
