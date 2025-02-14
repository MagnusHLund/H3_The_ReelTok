using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Auth;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    internal class AuthService : BaseService, IAuthService
    {
        private const string AuthMicroServiceBaseUrl = "http://localhost:5003/auth";
        private readonly IHttpService _httpService;

        public AuthService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> LogOutUser()
        {
            ServiceLogOutUserRequestDto requestDto = new ServiceLogOutUserRequestDto();
            string targetUrl = $"{AuthMicroServiceBaseUrl}/logout";

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceLogOutUserRequestDto, ServiceLogOutUserResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            if (response.Success && response is ServiceLogOutUserResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleExceptions(response);
        }

        public async Task<Guid> GetUserIdByToken()
        {
            ServiceGetUserIdByTokenRequestDto requestDto = new ServiceGetUserIdByTokenRequestDto();
            string targetUrl = $"{AuthMicroServiceBaseUrl}/getUserIdByToken";

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceGetUserIdByTokenResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            if (response.Success && response is ServiceGetUserIdByTokenResponseDto responseDto)
            {
                return responseDto.UserId;
            }

            throw HandleExceptions(response);
        }
    }
}
