using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.Entities
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