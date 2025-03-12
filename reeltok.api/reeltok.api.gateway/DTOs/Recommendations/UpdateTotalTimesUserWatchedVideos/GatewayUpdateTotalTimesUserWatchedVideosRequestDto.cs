using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Recommendations.UpdateTotalTimesUserWatchedVideos
{
    public class GatewayUpdateTotalTimesUserWatchedVideosRequestDto
    {
        [Required]
        [JsonProperty("VideoIds")]
        public List<Guid> VideoIds { get; set; }

        public GatewayUpdateTotalTimesUserWatchedVideosRequestDto(List<Guid> videoIds)
        {
            VideoIds = videoIds;
        }
    }
}