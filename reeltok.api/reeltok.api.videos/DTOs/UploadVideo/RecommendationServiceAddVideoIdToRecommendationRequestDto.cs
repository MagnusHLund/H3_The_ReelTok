public class RecommendationServiceAddVideoIdToRecommendationRequestDto
{
    public Guid VideoId { get; set; }
    public byte Category { get; set; }

    public RecommendationServiceAddVideoIdToRecommendationRequestDto(Guid videoId, byte category)
    {
        VideoId = videoId;
        Category = category;
    }
}
