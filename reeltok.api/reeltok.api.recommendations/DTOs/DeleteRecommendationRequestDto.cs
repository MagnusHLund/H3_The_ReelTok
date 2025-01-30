using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    internal class DeleteRecommendationRequestDto
    {
        [Required]
        internal string Category { get; private set; }
        [Required]
        internal Guid UserId { get; private set; }

        internal DeleteRecommendationRequestDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}
