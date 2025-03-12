using reeltok.api.users.DTOs;
using reeltok.api.users.Exceptions;
using reeltok.api.users.DTOs.Login;
using reeltok.api.users.DTOs.CreateUser;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Factories;
using reeltok.api.users.DTOs.GetUserInterest;

namespace reeltok.api.users.Services
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

        public async Task CreateUserInAuthApiAsync(Guid userId, string password)
        {
            AuthServiceCreateUserRequestDto requestDto = new AuthServiceCreateUserRequestDto(userId, password);
            Uri targetUrl = _endpointFactory.GetAuthApiUrl("users/signup");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<AuthServiceCreateUserRequestDto, AuthServiceCreateUserResponseDto>(
                requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is AuthServiceCreateUserResponseDto)
            {
                return;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task CreateUserInRecommendationsApiAsync(Guid userId, byte userInterests)
        {
            RecommendationsServiceCreateUserRequestDto requestDto =
                new RecommendationsServiceCreateUserRequestDto(userId, userInterests);
            Uri targetUrl = _endpointFactory.GetRecommendationsApiUrl("users");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<RecommendationsServiceCreateUserRequestDto, RecommendationsServiceCreateUserResponseDto>(
                requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is RecommendationsServiceCreateUserResponseDto)
            {
                return;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task LoginUserInAuthApiAsync(Guid userId, string password)
        {
            AuthServiceLoginRequestDto requestDto = new AuthServiceLoginRequestDto(userId, password);
            Uri targetUrl = _endpointFactory.GetAuthApiUrl("auth");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<AuthServiceLoginRequestDto, AuthServiceLoginResponseDto>(
                requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is AuthServiceLoginResponseDto)
            {
                return;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<byte> GetUserInterestFromRecommendationsApiAsync(Guid userId)
        {
            RecommendationServiceGetUserInterestRequestDto requestDto =
                new RecommendationServiceGetUserInterestRequestDto(userId);
            Uri targetUrl = _endpointFactory.GetRecommendationsApiUrl("users");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<RecommendationServiceGetUserInterestRequestDto, RecommendationServiceGetUserInterestResponseDto>(
                requestDto, targetUrl, HttpMethod.Get)
                .ConfigureAwait(false);

            if (response.Success && response is RecommendationServiceGetUserInterestResponseDto recommendationUserInterest)
            {
                return recommendationUserInterest.UserInterest;
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
