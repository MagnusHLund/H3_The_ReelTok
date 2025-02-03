using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Recommendations;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> UpdateRecommendedCategory([FromBody] ChangeRecommendedCategoryRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Category))
            {
                return BadRequest(new FailureResponseDto("Invalid Category!"));
            }

            bool success = await _recommendationsService.ChangeRecommendedCategory(request.Category);
            return Ok(new ChangeRecommendedCategoryResponseDto(success));
        }
    }
}