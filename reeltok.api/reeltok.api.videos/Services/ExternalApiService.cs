using reeltok.api.videos.DTOs;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Exceptions;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;
using reeltok.api.videos.DTOs.UserLikedVideo;
using reeltok.api.videos.Interfaces.Services;
using reeltok.api.videos.Interfaces.Factories;
using reeltok.api.videos.DTOs.GetRecommendedVideos;
using reeltok.api.videos.DTOs.GetUserDetailsForVideo;

namespace reeltok.api.videos.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly IHttpService _httpService;
        private readonly IEndpointFactory _endpointFactory;

        public ExternalApiService(IHttpService httpService, IEndpointFactory endpointFactory)
        {
            _httpService = httpService;
            _endpointFactory = endpointFactory;
        }

        public async Task<List<Guid>> GetRecommendedVideoIdsAsync(Guid userId, byte amount)
        {
            ServiceGetRecommendedVideosRequestDto requestDto = new ServiceGetRecommendedVideosRequestDto(userId, amount);
            Uri targetUrl = _endpointFactory.GetRecommendationsApiUrl("recommendations");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetRecommendedVideosRequestDto, ServiceGetRecommendedVideosResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetRecommendedVideosResponseDto responseDto)
            {
                return responseDto.VideoIdList;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<List<VideoCreatorEntity>> GetVideoCreatorDetailsAsync(List<Guid> videoIds)
        {
            ServiceGetUserDetailsForVideoRequestDto requestDto = new ServiceGetUserDetailsForVideoRequestDto(videoIds);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("users");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetUserDetailsForVideoRequestDto, ServiceGetUserDetailsForVideoResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetUserDetailsForVideoResponseDto responseDto)
            {
                return responseDto.VideoCreators;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> LikeVideoAsync(Guid userId, Guid videoId)
        {
            ServiceAddLikeRequestDto requestDto = new ServiceAddLikeRequestDto(userId, videoId);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("likes");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceAddLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> RemoveLikeFromVideoAsync(Guid userId, Guid videoId)
        {
            ServiceRemoveLikeRequestDto requestDto = new ServiceRemoveLikeRequestDto(userId, videoId);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("likes");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(requestDto, targetUrl, HttpMethod.Delete)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceRemoveLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<List<HasUserLikedVideoEntity>> HasUserLikedVideosAsync(Guid userId, List<Guid> videoIds)
        {
            ServiceHasUserLikedVideosRequestDto requestDto = new ServiceHasUserLikedVideosRequestDto(userId, videoIds);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("likes");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceHasUserLikedVideosRequestDto, ServiceHasUserLikedVideosResponseDto>(requestDto, targetUrl, HttpMethod.Get)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceHasUserLikedVideosResponseDto responseDto)
            {
                return responseDto.HasUserLikedVideos;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        private static Exception HandleNetworkResponseExceptions(BaseResponseDto response)
        {
            if (response is FailureResponseDto failureResponse)
            {
                return new FailureNetworkResponseException(failureResponse.Message);
            }

            return new InvalidOperationException("An unknown error has occurred!");
        }
    }
}