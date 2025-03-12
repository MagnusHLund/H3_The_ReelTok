namespace reeltok.api.gateway.DTOs.Recommendations.UpdateTotalTimesUserWatchedVideos
{
    public class ServiceUpdateTotalTimesUserWatchedVideosRequestDto
    {
        public Guid UserId { get; set; }
        public List<Guid> VideoIds { get; set; }

        public ServiceUpdateTotalTimesUserWatchedVideosRequestDto(Guid userId, List<Guid> videoIds)
        {
            UserId = userId;
            VideoIds = videoIds;
        }
    }
}
