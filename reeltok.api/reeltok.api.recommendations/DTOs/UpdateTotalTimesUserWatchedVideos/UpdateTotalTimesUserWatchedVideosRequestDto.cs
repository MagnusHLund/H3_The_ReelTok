namespace reeltok.api.recommendations.DTOs.UpdateTotalTimesUserWatchedAVideo
{
    public class UpdateTotalTimesUserWatchedVideosRequestDto
    {
        public Guid UserId { get; set; }
        public List<Guid> VideoIds { get; set; }

        public UpdateTotalTimesUserWatchedVideosRequestDto(Guid userId, List<Guid> videoIds)
        {
            UserId = userId;
            VideoIds = videoIds;
        }
    }
}