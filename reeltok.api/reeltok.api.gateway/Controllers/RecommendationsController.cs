using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.DTOs.Recommendations.UpdateTotalTimesUserWatchedVideos;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationsService _recommendationsService;

        public RecommendationsController(IRecommendationsService recommendationsService)
        {
            _recommendationsService = recommendationsService;
        }

        [HttpPut("videos/watched")]
        public async Task<IActionResult> UpdateTotalTimesUserWatchedVideosAsync(
            [FromBody] GatewayUpdateTotalTimesUserWatchedVideosRequestDto request
        )
        {
            bool success = await _recommendationsService.UpdateTotalTimesUserWatchedVideosAsync(request.VideoIds)
                .ConfigureAwait(false);

            GatewayUpdateTotalTimesUserWatchedVideosResponseDto responseDto =
                new GatewayUpdateTotalTimesUserWatchedVideosResponseDto(success);

            return Ok(responseDto);
        }
    }
}
