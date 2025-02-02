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
            string targetUri = $"{AuthMicroServiceBaseUrl}/logout";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<LogOutUserRequestDto, LogOutUserResponseDto>(requestDto, targetUri, HttpMethod.Post);

            if (!response.Success)
            {
                throw new InvalidOperationException("Logout failed");
            }

            return response.Success;
        }

        public async Task<Guid> GetUserIdByToken()
        {
            GetUserIdByTokenRequestDto requestDto = new GetUserIdByTokenRequestDto();
            string targetUri = $"{AuthMicroServiceBaseUrl}/getUserIdByToken";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<GetUserIdByTokenRequestDto, GetUserIdByTokenResponseDto>(requestDto, targetUri, HttpMethod.Get);

            if (response.Success && response is GetUserIdByTokenResponseDto getUserIdResponse)
            {
                if (getUserIdResponse.UserId != Guid.Empty)
                {
                    return getUserIdResponse.UserId;
                }
                throw new InvalidOperationException("Invalid user ID");
            }

            if (response is FailureResponseDto failureResponse)
            {
                throw new InvalidOperationException(failureResponse.Message);
            }

            throw new InvalidOperationException("An unknown error has occurred!");
        }
    }
}