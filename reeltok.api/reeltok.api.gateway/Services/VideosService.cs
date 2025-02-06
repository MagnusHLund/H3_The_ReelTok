using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Videos.DeleteVideo;
using reeltok.api.gateway.DTOs.Videos.GetVideosForFeed;
using reeltok.api.gateway.DTOs.Videos.LikeVideo;
using reeltok.api.gateway.DTOs.Videos.RemoveLike;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Services
{
    internal class VideosService : BaseService, IVideosService
    {
        private const string VideosMicroServiceBaseUrl = "http://localhost:5002/videos";
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;
        public VideosService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }
        public async Task<bool> LikeVideo(Guid VideoId)
        {
            Guid userId = await _authService.GetUserIdByToken();

            ServiceAddLikeRequestDto requestDto = new ServiceAddLikeRequestDto(userId, VideoId);
            string targetUrl = $"{VideosMicroServiceBaseUrl}/AddLike";

            BaseResponseDto request = await _gatewayService.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            if (request.Success && request is ServiceAddLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleExceptions(request);
        }
        public async Task<bool> RemoveLikeFromVideo(Guid VideoId)
        {
            Guid userId = await _authService.GetUserIdByToken();

            ServiceRemoveLikeRequestDto requestDto = new ServiceRemoveLikeRequestDto(userId, VideoId);
            string targetUrl = $"{VideosMicroServiceBaseUrl}/RemoveLike";

            BaseResponseDto request = await _gatewayService.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            if (request.Success && request is ServiceRemoveLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleExceptions(request);
        }

        public async Task<List<Video>> GetVideosForFeed(byte amount)
        {
            Guid userId = await _authService.GetUserIdByToken();

            ServiceGetVideosForFeedRequestDto requestDto = new ServiceGetVideosForFeedRequestDto(userId, amount);
            string targetUrl = $"{VideosMicroServiceBaseUrl}/GetVideoFeed";

            BaseResponseDto request = await _gatewayService.ProcessRequestAsync<ServiceGetVideosForFeedRequestDto, ServiceGetVideosForFeedResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            if (request.Success && request is ServiceGetVideosForFeedResponseDto responseDto)
            {
                return responseDto.Videos;
            }

            throw HandleExceptions(request);
        }

        public async Task<Video> UploadVideo(VideoUpload video)
        {
            Guid userId = await _authService.GetUserIdByToken();

            ServiceUploadVideoRequestDto requestDto = new ServiceUploadVideoRequestDto(userId, video);
            string targetUrl = $"{VideosMicroServiceBaseUrl}/Upload";

            BaseResponseDto request = await _gatewayService.ProcessRequestAsync<ServiceUploadVideoRequestDto, ServiceUploadVideoResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            if (request.Success && request is ServiceUploadVideoResponseDto responseDto)
            {
                return responseDto.Video;
            }

            throw HandleExceptions(request);
        }

        public async Task<bool> DeleteVideo(Guid videoId)
        {
            Guid userId = await _authService.GetUserIdByToken();

            ServiceDeleteVideoRequestDto requestDto = new ServiceDeleteVideoRequestDto(userId, videoId);
            string targetUrl = $"{VideosMicroServiceBaseUrl}/Delete";

            BaseResponseDto request = await _gatewayService.ProcessRequestAsync<ServiceDeleteVideoRequestDto, ServiceDeleteVideoResponseDto>(requestDto, targetUrl, HttpMethod.Delete);

            if (request.Success && request is ServiceDeleteVideoResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleExceptions(request);
        }
    }
}