using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.Interfaces.Factories;
using reeltok.api.gateway.DTOs.Auth.LogOutUser;
using reeltok.api.gateway.DTOs.Auth.GetUserIdByToken;

namespace reeltok.api.gateway.Services
{
    internal class AuthService : BaseService, IAuthService
    {
        private readonly IHttpService _httpService;
        private readonly IEndpointFactory _endpointFactory;

        public AuthService(IHttpService httpService, IEndpointFactory endpointFactory)
        {
            _httpService = httpService;
            _endpointFactory = endpointFactory;
        }

        public async Task<bool> LogOutUser()
        {
            ServiceLogOutUserRequestDto requestDto = new ServiceLogOutUserRequestDto();
            Uri targetUrl = _endpointFactory.GetAuthApiUrl("auth/login");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceLogOutUserRequestDto, ServiceLogOutUserResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceLogOutUserResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<Guid> GetUserIdByAccessToken()
        {
            ServiceGetUserIdByTokenRequestDto requestDto = new ServiceGetUserIdByTokenRequestDto();
            Uri targetUrl = _endpointFactory.GetAuthApiUrl("users");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceGetUserIdByTokenResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetUserIdByTokenResponseDto responseDto)
            {
                return responseDto.UserId;
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
