using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.ValueObjects;
// using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Services
{
    public class WatchedVideoService : IWatchedVideoService
    {

        private readonly IWatchedVideoRepository _watchedVideoRepository;

        public WatchedVideoService(IWatchedVideoRepository watchedVideoRepository)
        {
            _watchedVideoRepository = watchedVideoRepository;
        }

        public Task<bool> AddWatchedVideoAsync(WatchedVideoEntity watchedVideoEntity)
        {
            try
            {
                bool IsAdded = _watchedVideoRepository.AddWatchedVideoAsync(watchedVideoEntity).Result;
                return Task.FromResult(IsAdded);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(bool, string)> UpdateTimeWatchedAsync(Guid videoId, Guid userId)
        {
            WatchedVideoEntity? watchRecord = await _watchedVideoRepository.GetByVideoAndUserAsync(videoId, userId);

            if (watchRecord == null)
            {
                return (false, "Couldn't find the record");
            }

            try
            {
                watchRecord.IncrementTimeWatched();

                bool isUpdated = await _watchedVideoRepository.UpdateWatchedVideoAsync(watchRecord);

                return (isUpdated, "User update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
