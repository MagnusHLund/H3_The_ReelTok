using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IWatchedVideoRepository
    {
        Task<bool> AddWatchedVideoAsync(WatchedVideoEntity watchedVideoEntity);
        Task<WatchedVideoEntity?> GetByVideoAndUserAsync(Guid videoId, Guid userId);
        Task<bool> UpdateWatchedVideoAsync(WatchedVideoEntity watchedVideoEntity);
    }
}
