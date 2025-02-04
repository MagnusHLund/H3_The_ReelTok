using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Auth;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    internal class AuthService : IAuthService
    {
        private const string AuthMicroServiceBaseUrl = "http://localhost:5003/auth";
        private readonly IGatewayService _gatewayService;

        public AuthService(IGatewayService gateway)
        {
            _gatewayService = gateway;
        }

        public async Task<bool> LogOutUser()
        {
            ServiceLogOutUserRequestDto requestDto = new ServiceLogOutUserRequestDto();
            string targetUrl = $"{AuthMicroServiceBaseUrl}/logout";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceLogOutUserRequestDto, ServiceLogOutUserResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            if (response.Success && response is ServiceLogOutUserResponseDto responseDto)
            {
                return responseDto.Success;
            }

            if (response is FailureResponseDto failureResponse)
            {
                throw new InvalidOperationException(failureResponse.Message);
            }

            throw new InvalidOperationException("An unknown error has occurred!");
        }

        public async Task<Guid> GetUserIdByToken()
        {
            ServiceGetUserIdByTokenRequestDto requestDto = new ServiceGetUserIdByTokenRequestDto();
            string targetUrl = $"{AuthMicroServiceBaseUrl}/getUserIdByToken";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceGetUserIdByTokenResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            if (response.Success && response is ServiceGetUserIdByTokenResponseDto responseDto)
            {
                return responseDto.UserId;
            }

            if (response is FailureResponseDto failureResponse)
            {
                throw new InvalidOperationException(failureResponse.Message);
            }

            throw new InvalidOperationException("An unknown error has occurred!");
        }
    }
}