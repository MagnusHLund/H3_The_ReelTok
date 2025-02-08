using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs
{
    public class UpdateRecommendationRequestDto
    {
        [Required]
        public List<RecommendedCategories> Category { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public UpdateRecommendationRequestDto(List<RecommendedCategories> category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}