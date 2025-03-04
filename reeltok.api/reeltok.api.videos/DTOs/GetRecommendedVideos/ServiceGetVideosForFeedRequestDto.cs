namespace reeltok.api.videos.DTOs.GetRecommendedVideos
{
    public class ServiceGetRecommendedVideosRequestDto
    {
        public Guid UserId { get; set; }
        public byte Amount { get; set; }

        public ServiceGetRecommendedVideosRequestDto(Guid userId, byte amount)
        {
            UserId = userId;
            Amount = amount;
        }

        public ServiceGetRecommendedVideosRequestDto() { }
    }
}
