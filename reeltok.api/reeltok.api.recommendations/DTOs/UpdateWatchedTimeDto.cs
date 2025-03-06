namespace reeltok.api.recommendations.DTOs
{
    public class UpdateWatchedTimeDto
    {
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }

        public UpdateWatchedTimeDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
