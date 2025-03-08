namespace reeltok.api.recommendations.DTOs.AddVideoRecommendation
{
    public class AddVideoRecommendationRequestDto
    {
        public Guid VideoId { get; set; }
        public int CategoryId { get; set; }

        public AddVideoRecommendationRequestDto(Guid videoId, int categoryId, bool success = true)
        {
            VideoId = videoId;
            CategoryId = categoryId;
        }
    }
}