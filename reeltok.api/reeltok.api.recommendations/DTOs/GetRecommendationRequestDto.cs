
using System.ComponentModel.DataAnnotations;


namespace reeltok.api.recommendations.DTOs
{
    public class GetRecommendationRequestDto
    {
        [Required]
        internal Guid UserId { get; set; }

        internal GetRecommendationRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}