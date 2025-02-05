using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs
{
    public class UpdateRecommendationRequestDto
    {
        [Required]
        public List<RecommendationsEnum> Category { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public UpdateRecommendationRequestDto(List<RecommendationsEnum> category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}