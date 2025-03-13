using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Videos.RemoveLike
{
    public class ServiceRemoveLikeRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        public ServiceRemoveLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
