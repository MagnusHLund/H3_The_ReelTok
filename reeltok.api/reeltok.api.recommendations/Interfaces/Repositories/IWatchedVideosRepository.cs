using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IWatchedVideosRepository
    {
        Task<List<WatchedVideoEntity>> GetExistingWatchedVideosAsync(Guid userId, List<Guid> videoIds);
        Task AddNewWatchedVideosAsync(List<WatchedVideoEntity> newWatchedVideos);
        Task UpdateWatchedVideosAsync(List<WatchedVideoEntity> watchedVideos);
    }
}
