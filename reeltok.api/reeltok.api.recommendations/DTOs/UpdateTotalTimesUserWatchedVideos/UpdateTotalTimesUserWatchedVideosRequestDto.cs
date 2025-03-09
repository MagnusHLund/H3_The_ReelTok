using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs.UpdateTotalTimesUserWatchedAVideo
{
    public class UpdateTotalTimesUserWatchedVideosRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("VideoIds")]
        public List<Guid> VideoIds { get; set; }

        public UpdateTotalTimesUserWatchedVideosRequestDto(Guid userId, List<Guid> videoIds)
        {
            UserId = userId;
            VideoIds = videoIds;
        }
    }
}