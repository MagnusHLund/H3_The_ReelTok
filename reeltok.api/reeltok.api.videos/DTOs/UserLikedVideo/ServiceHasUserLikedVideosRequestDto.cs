using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.UserLikedVideo
{
    public class ServiceHasUserLikedVideosRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("VideoIds")]
        public List<Guid> VideoIds { get; set; }

        public ServiceHasUserLikedVideosRequestDto(Guid userId, List<Guid> videoIds)
        {
            UserId = userId;
            VideoIds = videoIds;
        }
    }
}