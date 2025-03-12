using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Recommendations.UpdateTotalTimesUserWatchedVideos
{
    public class ServiceUpdateTotalTimesUserWatchedVideosRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [JsonProperty("VideoIds")]
        public List<Guid> VideoIds { get; set; }

        public ServiceUpdateTotalTimesUserWatchedVideosRequestDto(Guid userId, List<Guid> videoIds)
        {
            UserId = userId;
            VideoIds = videoIds;
        }
    }
}
