using reeltok.api.videos.DTOs;
using reeltok.api.videos.Utils;
using reeltok.api.videos.Mappers;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.DTOs.GetRecommendedVideos;
using reeltok.api.videos.DTOs.GetUserDetailsForVideo;
using reeltok.api.videos.Factories;

namespace reeltok.api.videos.Services
{
    public class VideosService : BaseService, IVideosService
    {
        private const string RecommendationsMicroServiceBaseUrl = "http://localhost:5004/api/recommendations";
        private const string UsersMicroServiceBaseUrl = "http://localhost:5001/api/users";
        private readonly IVideosRepository _videosRepository;
        private readonly IStorageService _storageService;
        private readonly ILikesService _likesService;
        private readonly IHttpService _httpService;

        public VideosService(IVideosRepository videosRepository, IStorageService storageService, ILikesService likesService, IHttpService httpService)
        {
            _videosRepository = videosRepository;
            _storageService = storageService;
            _likesService = likesService;
            _httpService = httpService;
        }

        public async Task DeleteVideoAsync(Guid userId, Guid videoId)
        {
            await _videosRepository.DeleteVideoAsync(userId, videoId).ConfigureAwait(false);

            string streamPath = VideoUtils.CreateStreamPath(userId, videoId);
            await _storageService.RemoveVideoFromFileServerAsync(streamPath).ConfigureAwait(false);
        }

        public async Task<List<VideoForFeedEntity>> GetVideosForFeedAsync(Guid userId, byte amount)
        {
            List<Guid> videoIds = await GetRecommendedVideoIdsAsync(userId, amount).ConfigureAwait(false);
            List<VideoEntity> videos = await _videosRepository.GetVideosForFeedAsync(videoIds).ConfigureAwait(false);

            List<VideoCreatorEntity> videoCreatorDetails = await GetVideoCreatorDetailsAsync(videoIds)
                .ConfigureAwait(false);

            List<VideoLikesEntity> videoLikes = await _likesService.GetVideoLikesAsync(userId, videoIds).ConfigureAwait(false);

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
            await StorageService.EnsureValidFileUploadAsync(video.VideoFile).ConfigureAwait(false);

            VideoEntity videoToUpload = VideoMapper.ConvertVideoUploadToVideoEntity(video, userId);
            VideoEntity videoEntity = await _videosRepository.CreateVideoAsync(videoToUpload).ConfigureAwait(false);

            await _storageService.UploadVideoToFileServerAsync(
                video.VideoFile, videoEntity.VideoId, videoEntity.UserId)
                .ConfigureAwait(false);

            return videoEntity;
        }

        private async Task<List<Guid>> GetRecommendedVideoIdsAsync(Guid userId, byte amount)
        {
            ServiceGetRecommendedVideosRequestDto requestDto = new ServiceGetRecommendedVideosRequestDto(userId, amount);
            Uri targetUrl = new Uri($"{RecommendationsMicroServiceBaseUrl}/getRecommendations");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetRecommendedVideosRequestDto, ServiceGetRecommendedVideosResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetRecommendedVideosResponseDto responseDto)
            {
                if (responseDto?.VideoIdList?.Count > 0)
                {
                    return responseDto.VideoIdList;
                }

                throw new InvalidOperationException("No videos to return!");
            }

            throw HandleNetworkResponseExceptions(response);
        }

        private async Task<List<VideoCreatorDetailsEntity>> GetVideoCreatorDetailsAsync(List<Guid> videoIds)
        {
            ServiceGetUserDetailsForVideoRequestDto requestDto = new ServiceGetUserDetailsForVideoRequestDto(videoIds);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/getUserDetails");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetUserDetailsForVideoRequestDto, ServiceGetUserDetailsForVideoResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetUserDetailsForVideoResponseDto responseDto)
            {
                return responseDto.VideoCreatorDetailsList;
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
