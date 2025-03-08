using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;


namespace reeltok.api.recommendations.Repositories
{
    public class RecommendationRepository : IRecommendationsRepository
    {
        private readonly RecommendationDbContext _context;

        public RecommendationRepository(RecommendationDbContext context)
        {
            _context = context;
        }

        // TODO: Remove if unused!
        public async Task<CategoryEntity?> GetCategoryAsync(CategoryType category)
        {
            return await _context.CategoryEntities
                .FirstOrDefaultAsync(c => c.CategoryDetails.CategoryName == category)
                .ConfigureAwait(false);
        }
    }
}
