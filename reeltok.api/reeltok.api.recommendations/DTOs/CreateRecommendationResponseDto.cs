

using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class CreateRecommendationResponseDto : BaseResponseDto
    {
        public CreateRecommendationResponseDto(bool success) : base(success)
        {
        }
    }
}
