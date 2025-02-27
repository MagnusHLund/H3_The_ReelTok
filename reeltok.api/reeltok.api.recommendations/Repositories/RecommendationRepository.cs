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

        public Task AddRecommendationForUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddRecommendationForVideoAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecommendationForUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
