using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    public class RecommendationsService : IRecommendationsService
    {
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;

        internal RecommendationsService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public void ChangeRecommendedCategory(string category) { }
    }
}