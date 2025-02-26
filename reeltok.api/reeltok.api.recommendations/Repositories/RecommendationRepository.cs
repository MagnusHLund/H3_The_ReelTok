using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Interfaces;

namespace reeltok.api.recommendations.Repositories
{
    public class RecommendationRepository : IRecommendationsRepository
    {
        private readonly RecommendationDbContext _Context;

        public RecommendationRepository(RecommendationDbContext recommendationDbContext)
        {
            _Context = recommendationDbContext;
        }
    }
}
