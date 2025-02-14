using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Enums;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.DTOs.Recommendations;

namespace reeltok.api.gateway.Services
{
    internal class RecommendationsService : BaseService, IRecommendationsService
    {
        private const string RecommendationsMicroServiceBaseUrl = "http://localhost:5004/api/recommendations";
        private readonly IAuthService _authService;
        private readonly IHttpService _httpService;

        internal RecommendationsService(IAuthService authService, IHttpService httpService)
        {
            _authService = authService;
            _httpService = httpService;
        }

        public Task<List<RecommendedCategories>> GetRecommendation(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateRecommendation(Recommendations recommendationCategory)
        {
            Guid userId = await _authService.GetUserIdByToken().ConfigureAwait(false);
            List<RecommendedCategories> RecommendationCategory = recommendationCategory.RecommendationCategory;

            ServiceChangeRecommendedCategoryRequestDto requestDto = new ServiceChangeRecommendedCategoryRequestDto(userId, RecommendationCategory);
            Uri targetUrl = new Uri($"{RecommendationsMicroServiceBaseUrl}/update");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceChangeRecommendedCategoryRequestDto, ServiceChangeRecommendedCategoryResponseDto>(requestDto, targetUrl, HttpMethod.Put).ConfigureAwait(false);

            if (response.Success && response is ServiceChangeRecommendedCategoryResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleExceptions(response);
        }
    }
}
