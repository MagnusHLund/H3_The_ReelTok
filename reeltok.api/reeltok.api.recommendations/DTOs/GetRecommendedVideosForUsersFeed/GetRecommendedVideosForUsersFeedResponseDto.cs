namespace reeltok.api.recommendations.DTOs.GetRecommendedVideosForUsersFeed
{
    public class GetRecommendedVideosForUsersFeedResponseDto : BaseResponseDto
    {
        public List<Guid> VideoIds { get; set; }

        public GetRecommendedVideosForUsersFeedResponseDto(List<Guid> videoIds, bool success = true) : base(success)
        {
            VideoIds = videoIds;
        }
    }
}