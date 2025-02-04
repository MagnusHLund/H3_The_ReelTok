
using System.ComponentModel.DataAnnotations;


namespace reeltok.api.recommendations.DTOs
{
    public class GetRecommendationRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        public GetRecommendationRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}