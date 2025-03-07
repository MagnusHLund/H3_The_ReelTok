using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.RemoveLike
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
