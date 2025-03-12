using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.Interfaces.Factories;
using reeltok.api.gateway.DTOs.Recommendations.UpdateTotalTimesUserWatchedVideos;

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

        public async Task<bool> UpdateTotalTimesUserWatchedVideosAsync(List<Guid> videoIds)
        {
            Guid userId = await _authService.GetUserIdByAccessToken().ConfigureAwait(false);

            ServiceUpdateTotalTimesUserWatchedVideosRequestDto requestDto =
                new ServiceUpdateTotalTimesUserWatchedVideosRequestDto(userId, videoIds);

            Uri targetUrl = _endpointFactory.GetRecommendationsApiUrl("videos/watched");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <ServiceUpdateTotalTimesUserWatchedVideosRequestDto, ServiceUpdateTotalTimesUserWatchedVideosResponseDto>(
                requestDto, targetUrl, HttpMethod.Put)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceUpdateTotalTimesUserWatchedVideosResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
