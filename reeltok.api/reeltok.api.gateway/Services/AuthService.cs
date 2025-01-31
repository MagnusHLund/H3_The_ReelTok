using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IGatewayService _gatewayService;
        internal AuthService(IGatewayService gateway)
        {
            _gatewayService = gateway;
        }

        public async Task<HttpResponseMessage> LogOutUser()
        {
            LogOutUserRequestDto requestDto = new LogOutUserRequestDto();

            string targetUri = "auth/logout";
            HttpResponseMessage response = await _gatewayService.ProcessRequestAsync<LogOutUserRequestDto, LogOutUserResponseDto>(requestDto, targetUri);

            return response;
        }

        public void UpdatePassword(string password)
        {

        }

        public Guid GetUserIdByToken()
        {
            return Guid.Empty;
        }

    }
}