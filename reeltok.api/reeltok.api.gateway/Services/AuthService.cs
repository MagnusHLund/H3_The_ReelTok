using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    internal class AuthService : IAuthService
    {
        private readonly GatewayService _gatewayService;
        internal AuthService(GatewayService gateway)
        {
            _gatewayService = gateway;
        }

        public void LogOutUser()
        {

        }

        public void UpdatePassword(string password)
        {

        }

        public Guid GetUserIdByToken() { }

    }
}