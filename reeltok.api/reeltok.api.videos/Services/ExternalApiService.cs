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
            RecommendationsServiceGetRecommendedVideosRequestDto requestDto = new RecommendationsServiceGetRecommendedVideosRequestDto(userId, amount);
            Uri targetUrl = _endpointFactory.GetRecommendationsApiUrl("recommendations");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<RecommendationsServiceGetRecommendedVideosRequestDto, RecommendedServiceGetRecommendedVideosResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is RecommendedServiceGetRecommendedVideosResponseDto responseDto)
            {
                return responseDto.VideoIdList;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<List<VideoCreatorEntity>> GetVideoCreatorDetailsAsync(List<Guid> videoIds)
        {
            UsersServiceGetUserDetailsForVideoRequestDto requestDto = new UsersServiceGetUserDetailsForVideoRequestDto(videoIds);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("users");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<UsersServiceGetUserDetailsForVideoRequestDto, UserServiceGetUserDetailsForVideoResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is UserServiceGetUserDetailsForVideoResponseDto responseDto)
            {
                return responseDto.VideoCreators;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> LikeVideoAsync(Guid userId, Guid videoId)
        {
            UserServiceAddLikeRequestDto requestDto = new UserServiceAddLikeRequestDto(userId, videoId);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("likes");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<UserServiceAddLikeRequestDto, UserServiceAddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is UserServiceAddLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> RemoveLikeFromVideoAsync(Guid userId, Guid videoId)
        {
            UserServiceRemoveLikeRequestDto requestDto = new UserServiceRemoveLikeRequestDto(userId, videoId);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("likes");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<UserServiceRemoveLikeRequestDto, UserServiceRemoveLikeResponseDto>(requestDto, targetUrl, HttpMethod.Delete)
                .ConfigureAwait(false);

            if (response.Success && response is UserServiceRemoveLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<List<HasUserLikedVideoEntity>> HasUserLikedVideosAsync(Guid userId, List<Guid> videoIds)
        {
            UserServiceHasUserLikedVideosRequestDto requestDto = new UserServiceHasUserLikedVideosRequestDto(userId, videoIds);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("likes");

            BaseResponseDto response = await _httpService
                .ProcessRequestAsync<
                    UserServiceHasUserLikedVideosRequestDto,
                    UserServiceHasUserLikedVideosResponseDto
                >
                (
                    requestDto,
                    targetUrl,
                    HttpMethod.Get
                )
                .ConfigureAwait(false);

            if (response.Success && response is UserServiceHasUserLikedVideosResponseDto responseDto)
            {
                return responseDto.HasUserLikedVideos;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> AddVideoToRecommendationsApiAsync(Guid videoId, byte category)
        {
            RecommendationServiceAddVideoIdToRecommendationRequestDto requestDto = new RecommendationServiceAddVideoIdToRecommendationRequestDto(videoId, category);

            Uri targetUrl = _endpointFactory.GetRecommendationsApiUrl("recommendations");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<RecommendationServiceAddVideoIdToRecommendationRequestDto, RecommendationServiceAddVideoIdToRecommendationResponseDto>(
                requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response is RecommendationServiceAddVideoIdToRecommendationResponseDto responseDto)
            {
                return responseDto.Success;
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
