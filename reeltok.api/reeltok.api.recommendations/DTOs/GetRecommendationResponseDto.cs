using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs
{
    public class GetRecommendationResponseDto : BaseResponseDto
    {
        [Required]
        public List<RecommendationsEnum> Recommendations;

        public GetRecommendationResponseDto(List<RecommendationsEnum> recommendations, bool success) : base(success)
        {
            Recommendations = recommendations;
        }
    }
}