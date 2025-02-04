using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs
{
    internal class GetRecommendationResponseDto : BaseResponseDto
    {
        [Required]
        internal List<RecommendationsEnum> Recommendations;

        internal GetRecommendationResponseDto(List<RecommendationsEnum> recommendations, bool success) : base(success)
        {
            Recommendations = recommendations;
        }
    }
}