using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.DTOs.Videos.LikeVideo;
using reeltok.api.gateway.DTOs.Videos.RemoveLike;
using reeltok.api.gateway.DTOs.Videos.DeleteVideo;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;
using reeltok.api.gateway.DTOs.Videos.GetVideosForFeed;

namespace reeltok.api.gateway.Services
{
    internal class VideosService : BaseService, IVideosService
    {
        private const string VideosMicroServiceBaseUrl = "http://localhost:5002/api/videos";
        private readonly IAuthService _authService;
        private readonly IHttpService _httpService;
        public VideosService(IAuthService authService, IHttpService httpService)
        {
            _authService = authService;
            _httpService = httpService;
        }
        public async Task<bool> LikeVideo(Guid VideoId)
        {
            Guid userId = await _authService.GetUserIdByToken().ConfigureAwait(false);

            ServiceAddLikeRequestDto requestDto = new ServiceAddLikeRequestDto(userId, VideoId);
            Uri targetUrl = new Uri($"{VideosMicroServiceBaseUrl}/AddLike");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceAddLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleExceptions(response);
        }
        public async Task<bool> RemoveLikeFromVideo(Guid VideoId)
        {
            Guid userId = await _authService.GetUserIdByToken().ConfigureAwait(false);

            ServiceRemoveLikeRequestDto requestDto = new ServiceRemoveLikeRequestDto(userId, VideoId);
            Uri targetUrl = new Uri($"{VideosMicroServiceBaseUrl}/RemoveLike");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceRemoveLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleExceptions(response);
        }

        public async Task<List<Video>> GetVideosForFeed(byte amount)
        {
            Guid userId = await _authService.GetUserIdByToken().ConfigureAwait(false);

            ServiceGetVideosForFeedRequestDto requestDto = new ServiceGetVideosForFeedRequestDto(userId, amount);
            Uri targetUrl = new Uri($"{VideosMicroServiceBaseUrl}/GetVideoFeed");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetVideosForFeedRequestDto, ServiceGetVideosForFeedResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetVideosForFeedResponseDto responseDto)
            {
                return responseDto.Videos;
            }

            throw HandleExceptions(response);
        }

        public async Task<Video> UploadVideo(VideoUpload video)
        {
            Guid userId = await _authService.GetUserIdByToken().ConfigureAwait(false);

            ServiceUploadVideoRequestDto requestDto = new ServiceUploadVideoRequestDto(userId, video);
            Uri targetUrl = new Uri($"{VideosMicroServiceBaseUrl}/Upload");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceUploadVideoRequestDto, ServiceUploadVideoResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceUploadVideoResponseDto responseDto)
            {
                return responseDto.Video;
            }

            throw HandleExceptions(response);
        }

        public async Task<bool> DeleteVideo(Guid videoId)
        {
            Guid userId = await _authService.GetUserIdByToken().ConfigureAwait(false);

            ServiceDeleteVideoRequestDto requestDto = new ServiceDeleteVideoRequestDto(userId, videoId);
            Uri targetUrl = new Uri($"{VideosMicroServiceBaseUrl}/Delete");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceDeleteVideoRequestDto, ServiceDeleteVideoResponseDto>(requestDto, targetUrl, HttpMethod.Delete).ConfigureAwait(false);

            if (response.Success && response is ServiceDeleteVideoResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleExceptions(response);
        }

        // TODO: implement lazy loading for this request, and also some others. Also add tests for this
        public async Task<List<Video>> GetVideosForProfile(Guid userId, byte amountToReturn, uint amountReceived)
        {
            ServiceGetVideosForFeedRequestDto requestDto = new ServiceGetVideosForFeedRequestDto(userId, amountToReturn);
            Uri targetUrl = new Uri($"{VideosMicroServiceBaseUrl}/GetVideosForProfile");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetVideosForFeedRequestDto, ServiceGetVideosForFeedResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetVideosForFeedResponseDto responseDto)
            {
                return responseDto.Videos;
            }

            throw HandleExceptions(response);
        }
    }
}
