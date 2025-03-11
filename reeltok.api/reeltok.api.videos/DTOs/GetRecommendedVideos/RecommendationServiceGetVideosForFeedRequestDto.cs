using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetRecommendedVideos
{
    public class RecommendationsServiceGetRecommendedVideosRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Amount")]
        public byte Amount { get; set; }

        public RecommendationsServiceGetRecommendedVideosRequestDto(Guid userId, byte amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}
