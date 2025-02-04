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
            LogOutUserRequestDto requestDto = new LogOutUserRequestDto();
            string targetUrl = $"{AuthMicroServiceBaseUrl}/logout";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<LogOutUserRequestDto, LogOutUserResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            if (response.Success && response is LogOutUserResponseDto responseDto)
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
            GetUserIdByTokenRequestDto requestDto = new GetUserIdByTokenRequestDto();
            string targetUrl = $"{AuthMicroServiceBaseUrl}/getUserIdByToken";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<GetUserIdByTokenRequestDto, GetUserIdByTokenResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            if (response.Success && response is GetUserIdByTokenResponseDto responseDto)
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