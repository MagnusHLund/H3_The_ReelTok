using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.videos.DTOs.DeleteVideo
{
    public class RecommendationsServiceDeleteVideoRequestDto
    {
        [Required]
        [JsonProperty("videoId")]
        public Guid VideoId { get; set; }

        public RecommendationsServiceDeleteVideoRequestDto(Guid videoId)
        {
            VideoId = videoId;
        }
    }
}