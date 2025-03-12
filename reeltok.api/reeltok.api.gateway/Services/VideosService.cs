using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities.Videos;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.Interfaces.Factories;
using reeltok.api.gateway.DTOs.Videos.LikeVideo;
using reeltok.api.gateway.DTOs.Videos.RemoveLike;
using reeltok.api.gateway.DTOs.Videos.DeleteVideo;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;
using reeltok.api.gateway.DTOs.Videos.GetVideosForFeed;
using reeltok.api.gateway.DTOs.Videos.GetVideosForProfile;

namespace reeltok.api.gateway.Services
{
    public class VideosService : BaseService, IVideosService
    {
        private readonly IAuthService _authService;
        private readonly IHttpService _httpService;
        private readonly IEndpointFactory _endpointFactory;

        public VideosService(IAuthService authService, IHttpService httpService, IEndpointFactory endpointFactory)
        {
            _authService = authService;
            _httpService = httpService;
            _endpointFactory = endpointFactory;
        }

        public async Task<bool> LikeVideoAsync(Guid videoId)
        {
            Guid userId = await _authService.GetUserIdByAccessTokenAsync().ConfigureAwait(false);

            ServiceAddLikeRequestDto requestDto = new ServiceAddLikeRequestDto(userId, videoId);
            Uri targetUrl = _endpointFactory.GetVideosApiUrl("likes");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceAddLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> RemoveLikeFromVideoAsync(Guid videoId)
        {
            Guid userId = await _authService.GetUserIdByAccessTokenAsync().ConfigureAwait(false);

            ServiceRemoveLikeRequestDto requestDto = new ServiceRemoveLikeRequestDto(userId, videoId);
            Uri targetUrl = _endpointFactory.GetVideosApiUrl("likes");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(requestDto, targetUrl, HttpMethod.Delete)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceRemoveLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<List<VideoForFeedEntity>> GetVideosForFeedAsync(byte amount)
        {// TODO: ensure that the user does not require a user, to get a video
            Guid userId = await _authService.GetUserIdByAccessTokenAsync().ConfigureAwait(false);

            ServiceGetVideosForFeedRequestDto requestDto = new ServiceGetVideosForFeedRequestDto(userId, amount);
            Uri targetUrl = _endpointFactory.GetVideosApiUrl("videos/feed");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <ServiceGetVideosForFeedRequestDto, ServiceGetVideosForFeedResponseDto>(requestDto, targetUrl, HttpMethod.Get)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceGetVideosForFeedResponseDto responseDto)
            {
                return responseDto.Videos;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> UploadVideoAsync(VideoUpload video)
        {
            Guid userId = await _authService.GetUserIdByAccessTokenAsync().ConfigureAwait(false);
            video.UserId = userId;

            ServiceUploadVideoRequestDto requestDto = VideoMapper.ConvertVideoUploadToUploadVideoRequestDto(video);
            Uri targetUrl = _endpointFactory.GetVideosApiUrl("videos");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <ServiceUploadVideoRequestDto, ServiceUploadVideoResponseDto>(requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceUploadVideoResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> DeleteVideoAsync(Guid videoId)
        {
            Guid userId = await _authService.GetUserIdByAccessTokenAsync().ConfigureAwait(false);

            ServiceDeleteVideoRequestDto requestDto = new ServiceDeleteVideoRequestDto(userId, videoId);
            Uri targetUrl = _endpointFactory.GetVideosApiUrl("videos");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <ServiceDeleteVideoRequestDto, ServiceDeleteVideoResponseDto>(requestDto, targetUrl, HttpMethod.Delete)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceDeleteVideoResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<List<BaseVideoEntity>> GetVideosForProfileAsync(Guid userId, int pageNumber, byte pageSize)
        {
            ServiceGetVideosForProfileRequestDto requestDto = new
                ServiceGetVideosForProfileRequestDto(userId, pageNumber, pageSize);

            Uri targetUrl = _endpointFactory.GetVideosApiUrl("videos/profile");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <ServiceGetVideosForProfileRequestDto, ServiceGetVideosForProfileResponseDto>(
                requestDto, targetUrl, HttpMethod.Get)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceGetVideosForProfileResponseDto responseDto)
            {
                return responseDto.Videos;
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
