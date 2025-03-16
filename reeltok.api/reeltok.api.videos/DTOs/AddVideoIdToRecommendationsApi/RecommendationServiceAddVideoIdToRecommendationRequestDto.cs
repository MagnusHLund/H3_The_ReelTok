using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.AddVideoIdToRecommendationsApi
{
    public class RecommendationsServiceAddVideoIdToRecommendationsApiRequestDto
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("Category")]
        public byte Category { get; set; }

        public RecommendationsServiceAddVideoIdToRecommendationsApiRequestDto(Guid videoId, byte category)
        {
            VideoId = videoId;
            Category = category;
        }
    }
}
