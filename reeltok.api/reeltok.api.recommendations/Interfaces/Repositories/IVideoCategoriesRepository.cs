using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IVideoCategoriesRepository
    {
        Task<uint> AddVideoCategoryAsync(CategoryVideoCategoryEntity categoryVideoCategoryEntity);
        Task DeleteVideoAsync(Guid videoId);
    }
}