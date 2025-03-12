using reeltok.api.videos.Entities;
using reeltok.api.videos.Factories;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Interfaces.Services;

namespace reeltok.api.videos.Services
{
    public class LikesService : ILikesService
    {
        private readonly IExternalApiService _externalApiService;
        private readonly ILikesRepository _likesRepository;
        public LikesService(IExternalApiService externalApiService, ILikesRepository likesRepository)
        {
            _externalApiService = externalApiService;
            _likesRepository = likesRepository;
        }

        public async Task<bool> LikeVideoAsync(Guid userId, Guid videoId)
        {
            await _likesRepository.IncrementTotalLikesAsync(videoId).ConfigureAwait(false);

            try
            {
                return await _externalApiService.LikeVideoAsync(userId, videoId).ConfigureAwait(false);
            }
            catch
            {
                await _likesRepository.DecrementTotalLikesAsync(videoId).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> RemoveLikeFromVideoAsync(Guid userId, Guid videoId)
        {
            await _likesRepository.DecrementTotalLikesAsync(videoId).ConfigureAwait(false);

            try
            {
                return await _externalApiService.RemoveLikeFromVideoAsync(userId, videoId).ConfigureAwait(false);
            }
            catch
            {
                await _likesRepository.IncrementTotalLikesAsync(videoId).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<List<VideoLikesEntity>> GetLikesForVideos(Guid userId, List<Guid> videoIds)
        {
            List<HasUserLikedVideoEntity> hasUserLikedVideo = new List<HasUserLikedVideoEntity>();

            if (userId != Guid.Empty)
            {
                hasUserLikedVideo = await _externalApiService.HasUserLikedVideosAsync(
                    userId, videoIds).ConfigureAwait(false);
            }

            List<TotalVideoLikesEntity> videoTotalLikes = await _likesRepository.GetTotalLikesForVideosAsync(
                videoIds).ConfigureAwait(false);

            List<VideoLikesEntity> videoLikes = VideoFactory.CreateVideoLikesEntityList(
                videoIds, hasUserLikedVideo, videoTotalLikes);

            return videoLikes;
        }


    }
}
