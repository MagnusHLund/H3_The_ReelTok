using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    internal class DeleteRecommendationRequestDto
    {
        [Required]
        internal string Category { get; set; }
        [Required]
        internal Guid UserId { get; set; }

        internal DeleteRecommendationRequestDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}
