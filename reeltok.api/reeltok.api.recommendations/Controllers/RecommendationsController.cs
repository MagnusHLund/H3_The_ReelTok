using Microsoft.AspNetCore.Mvc;
using reeltok.api.auth.DTOs;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Interfaces.Services;

namespace reeltok.api.recommendations.RecommendationsServiceApi.Api.Controllers
{
    [ApiController]
    [Route("api/recommendations")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationsService _recommendationsService;

        public RecommendationsController(IRecommendationsService recommendationsService)
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

        [HttpGet("GetTopVideoByUserInterest")]
        public async Task<IActionResult> GetTopVideoByUserInterest([FromQuery] Guid userId, [FromQuery] int amount)
        {

            if (amount < 1)
            {
                return BadRequest(new FailureResponseDto("Amount must be greater than 1"));
            }
            else if (amount > 50)
            {
                return BadRequest(new FailureResponseDto("Amount must be less than 50"));
            }

            List<Guid> videoIds = await _recommendationsService.GetTopVideoByUserInterestAsync(userId, amount)
                .ConfigureAwait(false);

            AlgorithmResponseDTO response = new AlgorithmResponseDTO(videoIds);

            return Ok(response);
        }
    }
}
