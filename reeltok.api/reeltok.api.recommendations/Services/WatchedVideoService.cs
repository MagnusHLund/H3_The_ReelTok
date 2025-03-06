using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Interfaces.Services;

namespace reeltok.api.recommendations.Services
{
    public class WatchedVideoService : IWatchedVideoService
    {
        private readonly IWatchedVideoRepository _watchedVideoRepository;

        public WatchedVideoService(IWatchedVideoRepository watchedVideoRepository)
        {
            _watchedVideoRepository = watchedVideoRepository;
        }

        public async Task<(bool, string)> AddOrUpdateWatchedVideoAsync(WatchedVideoEntity watchedVideoEntity)
        {
            WatchedVideoEntity? existingRecord = await GetWatchedVideoAsync(
                watchedVideoEntity.WatchedVideoDetails.VideoId,
                watchedVideoEntity.WatchedVideoDetails.UserId);

            if (existingRecord != null)
            {
                return await UpdateTimeWatchedAsync(existingRecord);
            }
            else
            {
                return await CreateWatchedVideoAsync(watchedVideoEntity);
            }
        }

        private async Task<WatchedVideoEntity?> GetWatchedVideoAsync(Guid videoId, Guid userId)
        {
            return await _watchedVideoRepository.GetByVideoAndUserAsync(videoId, userId);
        }

        private async Task<(bool, string)> CreateWatchedVideoAsync(WatchedVideoEntity watchedVideoEntity)
        {
            try
            {
                bool isAdded = await _watchedVideoRepository.AddWatchedVideoAsync(watchedVideoEntity);
                return (isAdded, isAdded ? "Success" : "Failed to add watched video");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private async Task<(bool, string)> UpdateTimeWatchedAsync(WatchedVideoEntity watchRecord)
        {
            try
            {
                watchRecord.IncrementTimeWatched();
                bool isUpdated = await _watchedVideoRepository.UpdateWatchedVideoAsync(watchRecord);
                return (isUpdated, isUpdated ? "User update successful" : "Failed to update record");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
