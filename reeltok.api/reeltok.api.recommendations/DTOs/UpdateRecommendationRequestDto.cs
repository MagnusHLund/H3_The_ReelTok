using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    internal class UpdateRecommendationRequestDto
    {
        [Required]
        internal string Category { get; private set; }
        [Required]
        internal Guid UserId { get; private set; }

        internal UpdateRecommendationRequestDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}