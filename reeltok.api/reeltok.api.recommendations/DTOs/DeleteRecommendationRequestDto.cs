using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    public class DeleteRecommendationRequestDto
    {
        [Required]
        public string Category { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public DeleteRecommendationRequestDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}
