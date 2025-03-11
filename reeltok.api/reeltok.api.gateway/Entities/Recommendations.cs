using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.Entities
{
    public class Recommendations
    {
        public Guid UserId { get; set; }
        public List<CategoryType> RecommendationCategory { get; set; }

        public Recommendations(Guid userId, List<CategoryType> recommendationsEnum)
        {
            UserId = userId;
            RecommendationCategory = recommendationsEnum;
        }

        public void AddRecommendation(CategoryType recommendation)
        {
            RecommendationCategory.Add(recommendation);
        }
    }
}
