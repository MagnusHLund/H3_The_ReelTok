
using Microsoft.IdentityModel.Tokens;
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
        public async Task<List<RecommendedCategories>> GetRecommendation(Guid userId)
        {
            List<RecommendedCategories> recommendation = await _recommendationsRepository.GetRecommendationAsync(userId);

            if (recommendation.IsNullOrEmpty())
            {
                throw new InvalidOperationException("Yordan is Gay!");
            }

            return recommendation;
        }

        public Task<bool> UpdateRecommendation(Recommendations recommendation)
        {

            throw new InvalidOperationException("Invalid parameters provided.");
        }
    }
}