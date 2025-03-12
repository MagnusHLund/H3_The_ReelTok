using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Recommendations.UpdateTotalTimesUserWatchedVideos
{
    public class GatewayUpdateTotalTimesUserWatchedVideosRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public List<Guid> VideoIds { get; set; }

        public GatewayUpdateTotalTimesUserWatchedVideosRequestDto(Guid userId, List<Guid> videoIds)
        {
            UserId = userId;
            VideoIds = videoIds;
        }
    }
}