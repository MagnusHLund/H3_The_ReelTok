namespace reeltok.api.videos.DTOs.AddVideoIdToRecommendationsApi
{
    public class RecommendationsServiceAddVideoIdToRecommendationsApiRequestDto
    {
        public Guid VideoId { get; set; }
        public byte Category { get; set; }

        public RecommendationsServiceAddVideoIdToRecommendationsApiRequestDto(Guid videoId, byte category)
        {
            VideoId = videoId;
            Category = category;
        }
    }
}
