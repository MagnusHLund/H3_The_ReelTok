using reeltok.api.videos.DTOs;

public class RecommendationServiceAddVideoIdToRecommendationResponseDto : BaseResponseDto
{
    public bool IsAdded { get; set; }

    public RecommendationServiceAddVideoIdToRecommendationResponseDto(bool isAdded, bool success = true) : base(success)
    {
        IsAdded = isAdded;
    }
}
