using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetRecommendedVideos
{
    public class ServiceGetRecommendedVideosRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Amount")]
        public byte Amount { get; set; }

        public ServiceGetRecommendedVideosRequestDto(Guid userId, byte amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}
