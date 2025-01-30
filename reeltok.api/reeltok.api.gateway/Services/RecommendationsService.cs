using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    public class RecommendationsService : IRecommendationsService
    {
        private readonly AuthService _authService;
        private readonly GatewayService _gatewayService;

        internal RecommendationsService(AuthService authService, GatewayService gatewayService) { }

        public void ChangeRecommendedCategory(string category) { }
    }
}