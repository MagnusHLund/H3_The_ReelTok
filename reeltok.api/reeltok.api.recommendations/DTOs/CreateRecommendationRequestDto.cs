using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    public class CreateRecommendationRequestDto
    {
        public string Category { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public CreateRecommendationRequestDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}