using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Data;
using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;


namespace reeltok.api.recommendations.Repositories
{
    public class RecommendationRepository : IRecommendationsRepository
    {
        private readonly RecommendationDbContext _context;

        public RecommendationRepository(RecommendationDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryEntity?> GetCategoryAsync(RecommendedCategories category)
        {
            return await _context.CategoryEntities
                .FirstOrDefaultAsync(c => c.CategoryDetails.CategoryName == category)
                .ConfigureAwait(false);
        }
    }
}
