using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs
{
    public class GetVideosForFeedRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Amount")]
        public byte Amount { get; set; }

        public GetVideosForFeedRequestDto(Guid userId, byte amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}
