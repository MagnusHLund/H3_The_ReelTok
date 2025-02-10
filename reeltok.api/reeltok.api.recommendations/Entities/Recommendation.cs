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
        public List<RecommendedCategories> RecommendationCategory { get; set; }

        public Recommendations(Guid userId, List<RecommendedCategories> recommendationsEnum)
        {
            UserId = userId;
            RecommendationCategory = recommendationsEnum;
        }

        public void AddRecommendation(RecommendedCategories recommendation)
        {
            RecommendationCategory.Add(recommendation);
        }
    }

}

