namespace reeltok.api.recommendations.DTOs
{
    public class AddVideoRecommendationResponseDto : BaseResponseDto
    {
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }

        public AddVideoRecommendationResponseDto(Guid userId, int categoryId, bool success = true) : base(success)
        {
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}
