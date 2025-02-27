using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Interfaces;

namespace reeltok.api.recommendations.Services
{
    public class RecommendationsService : IRecommendationsService
    {
        private readonly IRecommendationsRepository _recommendationsRepository;
        public RecommendationsService(IRecommendationsRepository recommendationsRepository)
        {
            _recommendationsRepository = recommendationsRepository;
        }

        public Task AddRecommendationForUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddRecommendationForVideoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RecommendedCategories> GetRecommendedCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecommendationForUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
