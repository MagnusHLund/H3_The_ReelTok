using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    internal class CreateRecommendationRequestDto
    {
        internal string Category { get; set; }
        [Required]
        internal Guid UserId { get; set; }

        internal CreateRecommendationRequestDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}