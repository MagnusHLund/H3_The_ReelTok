using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs
{
    public class DeleteVideoRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        public DeleteVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
