using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Entities
{
    public class Recommendations
    {
        public Guid UserId { get; set; }
        public List<RecommendationsEnum> RecommendationCategory { get; set; }

        public Recommendations(Guid userId, List<RecommendationsEnum> recommendationsEnum)
        {
            UserId = userId;
            RecommendationCategory = recommendationsEnum;
        }

        public void AddRecommendation(RecommendationsEnum recommendation)
        {
            RecommendationCategory.Add(recommendation);
        }
    }

}

