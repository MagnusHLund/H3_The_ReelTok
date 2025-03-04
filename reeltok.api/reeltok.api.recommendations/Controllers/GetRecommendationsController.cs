using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Interfaces.Services;

namespace reeltok.api.recommendations.RecommendationsServiceApi.Api.Controllers
{
    [ApiController]
    [Route("api/recommendateions")]
    public class GetRecommendationsController : ControllerBase
    {
        private readonly IRecommendationsService _recommendationsService;

        public GetRecommendationsController(IRecommendationsService recommendationsService)
        {
            _recommendationsService = recommendationsService;
        }

        [HttpGet("GetRecommendations")]
        public async Task<IActionResult> GetRecommendation()
        {
            List<string> recommendations = await _recommendationsService.GetAllCategoriesAsync()
                .ConfigureAwait(false);

            bool success = true;
            return Ok(new GetRecommendationResponseDto(recommendations, success));
        }
    }
}
