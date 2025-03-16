using reeltok.api.videos.Utils;
using reeltok.api.videos.Mappers;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Factories;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.Interfaces.Services;

namespace reeltok.api.videos.Services
{
    public class VideosService : IVideosService
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IThumbnailService _thumbnailService;
        private readonly IVideosRepository _videosRepository;
        private readonly IStorageService _storageService;
        private readonly ILikesService _likesService;

        public VideosService(
            IExternalApiService externalApiService,
            IThumbnailService thumbnailService,
            IVideosRepository videosRepository,
            IStorageService storageService,
            ILikesService likesService
            )
        {
            _externalApiService = externalApiService;
            _thumbnailService = thumbnailService;
            _videosRepository = videosRepository;
            _storageService = storageService;
            _likesService = likesService;
        }

        public async Task<VideoEntity> GetVideoByIdAsync(Guid videoId)
        {
            VideoEntity video = await _videosRepository.GetVideoByIdAsync(videoId)
                .ConfigureAwait(false);

            return video;
        }

        public async Task DeleteVideoAsync(Guid userId, Guid videoId)
        {
            string filePathToDelete = await _videosRepository.DeleteVideoAsync(userId, videoId).ConfigureAwait(false);
            await _externalApiService.DeleteVideoFromRecommendationsApiAsync(videoId).ConfigureAwait(false);

            await _storageService.DeleteVideoFilesUsingSftpAsync(filePathToDelete).ConfigureAwait(false);
        }

        public async Task<List<VideoForFeedEntity>> GetVideosForFeedAsync(Guid userId, byte amount)
        {
            List<Guid> videoIds;

            if (userId != Guid.Empty)
            {
                videoIds = await _externalApiService.GetRecommendedVideoIdsAsync(userId, amount).ConfigureAwait(false);
            }
            else
            {
                videoIds = await _videosRepository.GetRandomVideoIdsAsync(userId, amount).ConfigureAwait(false);
            }

            if (!videoIds.Any())
            {
                throw new KeyNotFoundException($"No videos found, for user id {userId}");
            }

            List<VideoEntity> videos = await _videosRepository.GetVideosForFeedAsync(videoIds, amount).ConfigureAwait(false);
            List<Guid> videoCreatorIds = videos.ConvertAll(video => video.UserId);

            List<UserEntity> videoCreatorDetails = await _externalApiService.GetVideoCreatorDetailsAsync(videoCreatorIds)
                .ConfigureAwait(false);

            List<VideoLikesEntity> videoLikes = await _likesService.GetLikesForVideosAsync(userId, videoIds)
                .ConfigureAwait(false);

            List<VideoForFeedEntity> videosForFeed = VideoFactory.CreateVideoForFeedEntityList(
                videoIds,
                videos,
                videoCreatorDetails,
                videoLikes
            );

            return videosForFeed;
        }

        public async Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, uint pageNumber, byte pageSize)
        {
            List<VideoEntity> videosUploadedByUser = await _videosRepository
                .GetVideosForProfileAsync(userId, pageNumber, pageSize)
                .ConfigureAwait(false);

            return videosUploadedByUser;
        }

        public async Task<VideoEntity> UploadVideoAsync(VideoUpload video, Guid userId, byte category)
        {
            await VideoUtils.EnsureValidVideoFileAsync(video.VideoFile).ConfigureAwait(false);

            VideoEntity videoToUpload = VideoMapper.ConvertVideoUploadToVideoEntity(video, userId, video.VideoFile);
            VideoEntity videoEntity = await _videosRepository.CreateVideoAsync(videoToUpload).ConfigureAwait(false);

            IFormFile thumbnailFile = await _thumbnailService.GenerateVideoThumbnailAsync(video.VideoFile).ConfigureAwait(false);

            await _externalApiService.AddVideoToRecommendationsApiAsync(
                videoEntity.VideoId, category)
                .ConfigureAwait(false);

            try
            {
                await _storageService.UploadVideoFilesUsingSftpAsync(
                    video.VideoFile, thumbnailFile, videoEntity.VideoId, videoEntity.UserId)
                    .ConfigureAwait(false);
            }
            catch
            {
                await _externalApiService.DeleteVideoFromRecommendationsApiAsync(videoEntity.VideoId).ConfigureAwait(false);
                await _videosRepository.DeleteVideoAsync(videoEntity.VideoId, userId).ConfigureAwait(false);
                throw;
            }

            return videoEntity;
        }
    }
}
