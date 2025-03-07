using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.videos.DTOs.GetRecommendedVideos
{
    public class ServiceGetRecommendedVideosResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("VideoIdList")]
        public List<Guid> VideoIdList { get; set; }

        public ServiceGetRecommendedVideosResponseDto(List<Guid> videoIdList, bool success = true) : base(success)
        {
            VideoIdList = videoIdList;
        }
    }
}
