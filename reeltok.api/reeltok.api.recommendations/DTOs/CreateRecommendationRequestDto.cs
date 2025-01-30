using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    internal class CreateRecommendationRequestDto
    {
        [Required]
        internal string Category { get; private set; }
        [Required]
        internal Guid UserId { get; private set; }

        internal CreateRecommendationRequestDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}