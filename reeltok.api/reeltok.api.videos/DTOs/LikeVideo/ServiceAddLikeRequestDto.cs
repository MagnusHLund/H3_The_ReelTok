using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.LikeVideo
{
    public class ServiceAddLikeRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        public ServiceAddLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
