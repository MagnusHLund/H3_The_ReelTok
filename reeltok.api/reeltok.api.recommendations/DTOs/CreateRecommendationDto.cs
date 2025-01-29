namespace reeltok.api.recommendations.DTOs
{
    internal class CreateRecommendationDto
    {
        internal string? Category { get; private set; }
        internal Guid? UserId { get; private set; }

        internal CreateRecommendationDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}