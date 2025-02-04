using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Recommendations;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    internal class RecommendationsService : IRecommendationsService
    {
        private const string RecommendationsMicroServiceBaseUrl = "http://localhost:5004/recommendations";
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;

        internal RecommendationsService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public async Task<bool> ChangeRecommendedCategory(string category)
        {
            Guid userId = await _authService.GetUserIdByToken();

            ServiceChangeRecommendedCategoryRequestDto requestDto = new ServiceChangeRecommendedCategoryRequestDto(userId, category);
            string targetUrl = $"{RecommendationsMicroServiceBaseUrl}/update";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceChangeRecommendedCategoryRequestDto, ServiceChangeRecommendedCategoryResponseDto>(requestDto, targetUrl, HttpMethod.Put);

            if (response.Success && response is ServiceChangeRecommendedCategoryResponseDto responseDto)
            {
                return responseDto.Success;
            }

            if (response is FailureResponseDto failureResponse)
            {
                throw new InvalidOperationException(failureResponse.Message);
            }

            throw new InvalidOperationException("An unknown error has occurred!");
        }
    }
}