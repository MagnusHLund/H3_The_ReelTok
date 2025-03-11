using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Enums;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.DTOs.Recommendations.GetRecommendations;
using reeltok.api.gateway.DTOs.Recommendations.ChangeRecommendations;

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

        [HttpGet]
        public async Task<IActionResult> GetRecommendationAsync([FromBody] GatewayGetRecommendationsRequestDto request)
        {
            List<CategoryType> recommendations = await _recommendationsService.GetRecommendation(request.UserId).ConfigureAwait(false);

            if (Equals(recommendations.Count, 0))
            {
                return NoContent();
            }

            GatewayGetRecommendationsResponseDto responseDto = new GatewayGetRecommendationsResponseDto();
            return Ok(responseDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecommendationAsync([FromBody] GatewayChangeRecommendedCategoryRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId.ToString()))
            {
                return BadRequest(new FailureResponseDto("Invalid user id!"));
            }
            if (!Enum.IsDefined(typeof(CategoryType), request.Category))
            {
                return BadRequest(new FailureResponseDto("Invalid recommendations category!"));
            }

            bool success = await _recommendationsService.UpdateRecommendation(new Recommendations(request.UserId, request.Category)).ConfigureAwait(false);

            GatewayChangeRecommendedCategoryResponseDto requestDto = new GatewayChangeRecommendedCategoryResponseDto(success);
            return Ok(requestDto);

        }
    }
}
