using System.ComponentModel.DataAnnotations;
using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class GetRecommendationResponseDto : BaseResponseDto
    {
        [Required]
        public List<string> Recommendations { get; private set; }

        public GetRecommendationResponseDto(List<string> recommendations, bool success) : base(success)
        {
            Recommendations = recommendations;
        }
    }
}
