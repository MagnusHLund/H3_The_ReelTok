using reeltok.api.recommendations.Mappers;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Factories;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Services
{
    public class WatchedVideoService : IWatchedVideosService
    {
        private readonly IWatchedVideosRepository _watchedVideosRepository;

        public WatchedVideoService(IWatchedVideosRepository watchedVideosRepository)
        {
            _watchedVideosRepository = watchedVideosRepository;
        }

        public async Task UpdateTotalTimesUserWatchedVideosAsync(Guid userId, List<Guid> watchedVideoIds)
        {
            List<WatchedVideoEntity> existingWatchedVideos = await UpdateExistingVideosWatchCountAsync(userId, watchedVideoIds)
                .ConfigureAwait(false);

            List<Guid> newVideoIds = GetNewVideoIds(existingWatchedVideos, watchedVideoIds);

            await AddNewWatchedVideosAsync(userId, newVideoIds).ConfigureAwait(false);
        }

        private async Task<List<WatchedVideoEntity>> UpdateExistingVideosWatchCountAsync(Guid userId, List<Guid> watchedVideoIds)
        {
            List<WatchedVideoEntity> existingWatchedVideos = await _watchedVideosRepository
                .GetExistingWatchedVideosAsync(userId, watchedVideoIds)
                .ConfigureAwait(false);

            await _watchedVideosRepository.UpdateWatchedVideosAsync(existingWatchedVideos).ConfigureAwait(false);

            return existingWatchedVideos;
        }

        private static List<Guid> GetNewVideoIds(List<WatchedVideoEntity> existingWatchedVideos, List<Guid> watchedVideoIds)
        {
            List<Guid> existingVideoIds = WatchedVideoMapper.ConvertWatchedVideoEntityListToVideoIdList(existingWatchedVideos);
            List<Guid> newVideoIds = watchedVideoIds.Except(existingVideoIds).ToList();

            return newVideoIds;
        }

        private async Task AddNewWatchedVideosAsync(Guid userId, List<Guid> newVideoIds)
        {
            List<WatchedVideoEntity> newWatchedVideos = newVideoIds
                .Select(videoId => WatchedVideoFactory.CreateWatchedVideoEntity(userId, videoId))
                .ToList();

            await _watchedVideosRepository.AddNewWatchedVideosAsync(newWatchedVideos).ConfigureAwait(false);
        }
    }
}
