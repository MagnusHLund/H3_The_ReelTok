using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class UpdateWatchedTimeDto : BaseResponseDto
    {
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }

        public UpdateWatchedTimeDto(Guid userId, Guid videoId, bool success = true) : base(success)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
