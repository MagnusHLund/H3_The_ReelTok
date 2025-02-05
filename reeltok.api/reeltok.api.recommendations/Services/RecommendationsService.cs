
using reeltok.api.recommendations.Entities;
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
        public Task<List<RecommendationsEnum>> GetRecommendation(Guid userId)
        {

            throw new InvalidOperationException();
        }

        public Task<bool> UpdateRecommendation(Recommendations recommendation)
        {
            throw new InvalidOperationException("Invalid parameters provided.");
        }
    }
}