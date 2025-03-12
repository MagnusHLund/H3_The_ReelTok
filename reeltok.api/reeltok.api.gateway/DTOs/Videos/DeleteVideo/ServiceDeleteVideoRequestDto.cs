using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.DeleteVideo
{
    public class ServiceDeleteVideoRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        public ServiceDeleteVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
