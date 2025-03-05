using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Interfaces.repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class UserRecommendationRepository : IUserRecommendationRepository
    {
        private readonly RecommendationDbContext _context;
        private readonly IRecommendationsRepository _recommendationsRepository;
        public UserRecommendationRepository(RecommendationDbContext context, IRecommendationsRepository recommendationsRepository)
        {
            _context = context;

            _recommendationsRepository = recommendationsRepository;
        }

        public async Task<bool> AddRecommendationForUserAsync(UserInterestEntity userInterest, int categoryId)
        {
            CategoryEntity categoryEntity = await _recommendationsRepository
                .GetCategoryAsync((RecommendedCategories) categoryId);

            if (categoryEntity == null)
            {
                return false;
            }

            userInterest.Categories ??= new List<CategoryEntity>();

            userInterest.Categories.Add(categoryEntity);

            await _context.UserInterests.AddAsync(userInterest).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<UserInterestEntity?> GetUserInterestAsync(Guid userId)
        {
            return await _context.UserInterests
                .Include(ui => ui.Categories)
                .FirstOrDefaultAsync(ui => ui.UserInterestDetails.UserId == userId)
                ?? throw new KeyNotFoundException("User not found");
        }

        public Task<bool> UpdateRecommendationForUserAsync(Guid userId, RecommendedCategories updateCategory)
        {
            throw new NotImplementedException();
        }
    }
}
