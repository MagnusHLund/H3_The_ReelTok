public class ServiceAddVideoIdToRecommendationRequestDto
{
    public Guid VideoId { get; set; }
    public byte Category { get; set; }

    public ServiceAddVideoIdToRecommendationRequestDto(Guid videoId, byte category)
    {
        VideoId = videoId;
        Category = category;
    }
}