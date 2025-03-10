using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IVideosService
    {
        Task<List<Guid>> GetRecommendedVideosForUsersFeedAsync(Guid userId, byte amountOfVideos);
        Task<CategoryType> AddVideoCategoryAsync(Guid videoId, CategoryType videoCategory);
    }
}
