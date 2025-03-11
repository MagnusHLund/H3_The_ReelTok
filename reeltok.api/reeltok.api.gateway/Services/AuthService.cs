using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.DTOs.Auth.LogOutUser;
using reeltok.api.gateway.DTOs.Auth.GetUserIdByToken;

namespace reeltok.api.gateway.Services
{
    internal class AuthService : BaseService, IAuthService
    {
        private const string AuthMicroServiceBaseUrl = "http://localhost:5003/api/auth";
        private readonly IHttpService _httpService;

        public AuthService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> LogOutUser()
        {
            ServiceLogOutUserRequestDto requestDto = new ServiceLogOutUserRequestDto();
            Uri targetUrl = new Uri($"{AuthMicroServiceBaseUrl}/logout");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceLogOutUserRequestDto, ServiceLogOutUserResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceLogOutUserResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleExceptions(response);
        }

        public async Task<Guid> GetUserIdByToken()
        {
            ServiceGetUserIdByTokenRequestDto requestDto = new ServiceGetUserIdByTokenRequestDto();
            Uri targetUrl = new Uri($"{AuthMicroServiceBaseUrl}/getUserIdByToken");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceGetUserIdByTokenResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetUserIdByTokenResponseDto responseDto)
            {
                return responseDto.UserId;
            }

            throw HandleExceptions(response);
        }
    }
}
