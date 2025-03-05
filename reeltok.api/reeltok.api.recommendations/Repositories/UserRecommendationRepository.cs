using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class UserRecommendationRepository : IUserRecommendationRepository
    {
        private readonly RecommendationDbContext _context;
        public UserRecommendationRepository(RecommendationDbContext context)
        {
            _context = context;
        }

        public Task AddRecommendationForUserAsync(UserInterestEntity userInterest, int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecommendationForUserAsync(Guid userId, RecommendedCategories updateCategory)
        {
            throw new NotImplementedException();
        }
    }
}
