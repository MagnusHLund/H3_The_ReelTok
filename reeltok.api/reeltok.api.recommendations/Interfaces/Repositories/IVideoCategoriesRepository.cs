using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IVideoCategoriesRepository
    {
        Task<VideoEntity> AddVideoCategoryAsync(VideoEntity videoCategory);
    }
}