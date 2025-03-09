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
            try
            {
                // Increment total likes in the database
                await _likesRepository.IncrementTotalLikesAsync(videoId).ConfigureAwait(false);

                // Call the external API to register the like
                bool success = await _externalApiService.LikeVideoAsync(userId, videoId).ConfigureAwait(false);

                if (!success)
                {
                    // If external API call fails, decrement the like count from the database to keep consistency
                    await _likesRepository.DecrementTotalLikesAsync(videoId).ConfigureAwait(false);
                }

                return success;
            }
            catch (Exception ex)
            {
                // Handle any errors appropriately, for example, log the error or throw an exception
                throw new Exception("An error occurred while liking the video.", ex);
            }
        }

        public async Task<bool> RemoveLikeFromVideoAsync(Guid userId, Guid videoId)
        {
            try
            {
                // Decrement total likes in the database
                await _likesRepository.DecrementTotalLikesAsync(videoId).ConfigureAwait(false);

                // Call the external API to remove the like
                bool success = await _externalApiService.RemoveLikeFromVideoAsync(userId, videoId).ConfigureAwait(false);

                if (!success)
                {
                    // If external API call fails, increment the like count in the database to maintain consistency
                    await _likesRepository.IncrementTotalLikesAsync(videoId).ConfigureAwait(false);
                }

                return success;
            }
            catch (Exception ex)
            {
                // Handle any errors appropriately, for example, log the error or throw an exception
                throw new Exception("An error occurred while removing the like from the video.", ex);
            }
        }

        public async Task<List<VideoLikesEntity>> GetLikesForVideos(Guid userId, List<Guid> videoIds)
        {
            List<HasUserLikedVideoEntity> hasUserLikedVideo = await _externalApiService.HasUserLikedVideosAsync(
                userId, videoIds).ConfigureAwait(false);

            List<TotalVideoLikesEntity> videoTotalLikes = await _likesRepository.GetTotalLikesForVideosAsync(
                videoIds).ConfigureAwait(false);

            List<VideoLikesEntity> videoLikes = VideoFactory.CreateVideoLikesEntityList(
                videoIds, hasUserLikedVideo, videoTotalLikes);

            return videoLikes;
        }


    }
}
