namespace reeltok.api.recommendations.DTOs
{
    internal class DeleteRecommendationDto
    {
        internal string? Category { get; private set; }
        internal Guid? UserId { get; private set; }

        internal DeleteRecommendationDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}
