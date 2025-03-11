using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Recommendations.ChangeRecommendations
{
    public class GatewayChangeRecommendedCategoryResponseDto : BaseResponseDto
    {
        public GatewayChangeRecommendedCategoryResponseDto(bool success = true) : base(success) { }
    }
}
