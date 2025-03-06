using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IWatchedVideoService
    {
        Task<(bool, string)> AddOrUpdateWatchedVideoAsync(WatchedVideoEntity watchedVideoEntity);
    }
}
