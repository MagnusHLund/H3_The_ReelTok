using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Enums;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.Interfaces.Factories;
using reeltok.api.gateway.DTOs.Recommendations.ChangeRecommendations;

namespace reeltok.api.gateway.Services
{
    internal class RecommendationsService : BaseService, IRecommendationsService
    {
        private readonly IAuthService _authService;
        private readonly IHttpService _httpService;
        private readonly IEndpointFactory _endpointFactory;

        internal RecommendationsService(IAuthService authService, IHttpService httpService, IEndpointFactory endpointFactory)
        {
            _authService = authService;
            _httpService = httpService;
            _endpointFactory = endpointFactory;
        }

        public async Task<bool> UpdateRecommendation(Recommendations recommendationCategory)
        {
            Guid userId = await _authService.GetUserIdByAccessToken().ConfigureAwait(false);
            List<CategoryType> RecommendationCategory = recommendationCategory.RecommendationCategory;

            ServiceChangeRecommendedCategoryRequestDto requestDto = new
                ServiceChangeRecommendedCategoryRequestDto(userId, RecommendationCategory);

            Uri targetUrl = _endpointFactory.GetUsersApiUrl("");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <ServiceChangeRecommendedCategoryRequestDto, ServiceChangeRecommendedCategoryResponseDto>(
                requestDto, targetUrl, HttpMethod.Put)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceChangeRecommendedCategoryResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> UpdateTotalTimesUserWatchedVideosAsync(List<Guid> videoIds)
        {
            Guid userId = await _authService.GetUserIdByAccessToken().ConfigureAwait(false);

            ServiceUpdateTotalTimesUserWatchedVideosRequestDto requestDto = new
                ServiceUpdateTotalTimesUserWatchedVideosRequestDto();

            Uri targetUrl = _endpointFactory.GetRecommendationsApiUrl("videos/watched");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <ServiceUpdateTotalTimesUserWatchedVideosRequestDto, ServiceUpdateTotalTImesUserWatchedVideosResponseDto>(
                requestDto, targetUrl, HttpMethod.Put)
                .ConfigureAwait(false);
            if (response.Success && response is ServiceUpdateTotalTImesUserWatchedVideosResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
