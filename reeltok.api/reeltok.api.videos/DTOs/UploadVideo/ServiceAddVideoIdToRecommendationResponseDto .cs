using reeltok.api.videos.DTOs;

public class ServiceAddVideoIdToRecommendationResponseDto : BaseResponseDto
{
    public bool IsAdded { get; set; }

    public ServiceAddVideoIdToRecommendationResponseDto(bool isAdded, bool success = true) : base(success)
    {
        IsAdded = isAdded;
    }
}