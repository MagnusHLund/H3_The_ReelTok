using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IWatchedVideoService
    {
        Task<bool> AddWatchedVideoAsync(WatchedVideoEntity watchedVideoEntity);
        Task<(bool, string)> UpdateTimeWatchedAsync(Guid videoId, Guid userId);
    }
}
