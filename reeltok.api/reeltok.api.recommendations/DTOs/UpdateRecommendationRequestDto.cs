using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    public class UpdateRecommendationRequestDto
    {
        [Required]
        public string Category { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public UpdateRecommendationRequestDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}