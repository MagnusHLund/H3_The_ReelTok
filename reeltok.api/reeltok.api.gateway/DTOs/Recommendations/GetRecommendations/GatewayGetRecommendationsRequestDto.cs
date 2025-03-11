using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Recommendations.GetRecommendations
{
    public class GatewayGetRecommendationsRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        public GatewayGetRecommendationsRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}
