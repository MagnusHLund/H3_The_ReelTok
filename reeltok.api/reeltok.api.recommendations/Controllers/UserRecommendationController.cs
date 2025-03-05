using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.Interfaces.Services;

namespace reeltok.api.recommendations.Controllers
{
    [ApiController]
    [Route("api/User recommendations")]
    public class UserRecommendationController : ControllerBase
    {
        private readonly IUserRecommendationService _userRecommendationService;

        public UserRecommendationController(IUserRecommendationService userRecommendationService)
        {
            _userRecommendationService = userRecommendationService;
        }

        
    }
}
