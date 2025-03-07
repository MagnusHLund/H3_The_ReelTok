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
        private readonly IVideosRepository _videosRepository;
        private readonly IStorageService _storageService;
        private readonly ILikesService _likesService;

        public VideosService(
            IExternalApiService externalApiService,
            IVideosRepository videosRepository,
            IStorageService storageService,
            ILikesService likesService
            )
        {
            _externalApiService = externalApiService;
            _videosRepository = videosRepository;
            _storageService = storageService;
            _likesService = likesService;
        }

        public async Task DeleteVideoAsync(Guid userId, Guid videoId)
        {
            await _videosRepository.DeleteVideoAsync(userId, videoId).ConfigureAwait(false);

            string streamPath = VideoUtils.CreateStreamPath(userId, videoId);
            await _storageService.RemoveVideoFromFileServerAsync(streamPath).ConfigureAwait(false);
        }

        public async Task<List<VideoForFeedEntity>> GetVideosForFeedAsync(Guid userId, byte amount)
        {
            List<Guid> videoIds = await _externalApiService.GetRecommendedVideoIdsAsync(userId, amount).ConfigureAwait(false);
            List<VideoEntity> videos = await _videosRepository.GetVideosForFeedAsync(videoIds).ConfigureAwait(false);

            List<VideoCreatorEntity> videoCreatorDetails = await _externalApiService.GetVideoCreatorDetailsAsync(videoIds)
                .ConfigureAwait(false);

            List<VideoLikesEntity> videoLikes = await _likesService.GetLikesForVideos(userId, videoIds).ConfigureAwait(false);

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
            // Gets videos uploaded by the user that you're currently on
            List<VideoEntity> videosUploadedByUser = await _videosRepository
                .GetVideosForProfileAsync(userId, pageNumber, pageSize)
                .ConfigureAwait(false);

            return videosUploadedByUser;
        }

        public async Task<VideoEntity> UploadVideoAsync(VideoUpload video, Guid userId)
        {
            await VideoUtils.EnsureValidVideoFile(video.VideoFile).ConfigureAwait(false);

            VideoEntity videoToUpload = VideoMapper.ConvertVideoUploadToVideoEntity(video, userId);
            VideoEntity videoEntity = await _videosRepository.CreateVideoAsync(videoToUpload).ConfigureAwait(false);

            // TODO: Call recommendations api to add the video in its database

            await _storageService.UploadVideoToFileServerAsync(
                video.VideoFile, videoEntity.VideoId, videoEntity.UserId)
                .ConfigureAwait(false);

            return videoEntity;
        }
    }
}
