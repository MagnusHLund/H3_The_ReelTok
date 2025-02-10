using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Recommendations;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Enums;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationsService _recommendationsService;

        public RecommendationsController(IRecommendationsService recommendationsService)
        {
            _recommendationsService = recommendationsService;
        }

        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateRecommendation([FromBody] GatewayChangeRecommendedCategoryRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId.ToString()))
            {
                return BadRequest(new FailureResponseDto("Invalid user id!"));
            }
            if (!Enum.IsDefined(typeof(RecommendedCategories), request.Category))
            {
                return BadRequest(new FailureResponseDto("Invalid recommendations category!"));
            }

            bool success = await _recommendationsService.UpdateRecommendation(new Recommendations(request.UserId, request.Category));
            GatewayChangeRecommendedCategoryResponseDto requestDto = new GatewayChangeRecommendedCategoryResponseDto(success);

            return Ok(requestDto);

        }


        [HttpGet("GetRecommendations")]
        public async Task<IActionResult> GetRecommendation([FromBody] GatewayChangeRecommendedCategoryRequestDto request)
        {
            List<RecommendedCategories> recommendations = await _recommendationsService.GetRecommendation(request.UserId);

            if (Equals(recommendations.Count, 0))
            {
                return NoContent();
            }

            bool success = true;
            GatewayChangeRecommendedCategoryResponseDto responseDto = new GatewayChangeRecommendedCategoryResponseDto(success);

            return Ok(responseDto);
        }

        /* //   TODO: Implement this
                [HttpGet]
                [Route("GetCategories")]
                public async Task<IActionResult> GetRecommendedCategories([FromBody] GatewayGetRecommendationsRequestDto request)
                {
                    var categories = await _recommendationsService.GetRecommendedCategories();
                    return Ok(new GatewayGetRecommendationsResponseDto(categories));
                }
        */
    }
}

