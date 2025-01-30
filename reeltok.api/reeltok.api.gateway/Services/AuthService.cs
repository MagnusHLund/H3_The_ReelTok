using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            HttpRequest httpRequest = new HttpRequest(HttpMethod.Post, "auth/logout");
            HttpResponseMessage response = await _gatewayService.ProcessRequest(httpRequest);

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