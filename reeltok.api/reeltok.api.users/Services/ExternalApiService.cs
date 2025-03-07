using reeltok.api.users.DTOs;
using reeltok.api.users.Exceptions;
using reeltok.api.users.DTOs.Login;
using reeltok.api.users.DTOs.CreateUser;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Factories;

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
            // TODO: Remove this return, once auth api is ready
            return;

            AuthServiceCreateUserRequestDto requestDto = new AuthServiceCreateUserRequestDto(userId, password);
            Uri targetUrl = _endpointFactory.GetAuthApiUrl(""); // TODO: @MagnusHLund correct the endpoint

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
            // TODO: Remove this return, once recommendations api is ready
            return;

            RecommendationsServiceCreateUserRequestDto requestDto = new RecommendationsServiceCreateUserRequestDto(userId, userInterests);
            Uri targetUrl = _endpointFactory.GetRecommendationsApiUrl(""); // TODO: @MagnusHLund correct the endpoint

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
            // TODO: Remove this return, once auth api is ready
            return;

            AuthServiceLoginRequestDto requestDto = new AuthServiceLoginRequestDto(userId, password);
            Uri targetUrl = _endpointFactory.GetAuthApiUrl(""); // TODO: @MagnusHLund correct the endpoint

            BaseResponseDto response = await _httpService.ProcessRequestAsync<AuthServiceLoginRequestDto, AuthServiceLoginResponseDto>(
                requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is AuthServiceLoginResponseDto)
            {
                return;
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