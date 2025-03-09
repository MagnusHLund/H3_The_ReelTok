using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class VideoCategoriesRepository : IVideoCategoriesRepository
    {
        private readonly RecommendationDbContext _context;

        public VideoCategoriesRepository(RecommendationDbContext context)
        {
            _context = context;
        }

        public async Task<VideoEntity> AddVideoCategoryAsync(VideoEntity videoCategory)
        {
            VideoEntity savedVideoCategory = (await _context.VideoCategories.AddAsync(videoCategory)).Entity;
            await _context.SaveChangesAsync();

            return savedVideoCategory;
        }
    }
}