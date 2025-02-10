using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Interfaces;

namespace reeltok.api.recommendations.RecommendationsServiceApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        public readonly IRecommendationsService _recommendationsService;

        public RecommendationsController(IRecommendationsService recommendationService)
        {
            _recommendationsService = recommendationService;
        }

        [HttpGet("GetRecommendations")]
        public async Task<IActionResult> GetRecommendation([FromBody] GetRecommendationRequestDto request)
        {
            List<RecommendedCategories> recommendations = await _recommendationsService.GetRecommendation(request.UserId);

            bool success = true;
            return Ok(new GetRecommendationResponseDto(recommendations, success));
        }

        [HttpPut("UpdateRecommendations")]

        public async Task<IActionResult> UpdateRecommendation([FromBody] UpdateRecommendationRequestDto request)
        {
            Recommendations recommendations = new Recommendations(request.UserId, request.Category);

            bool success = await _recommendationsService.UpdateRecommendation(recommendations);
            return Ok(new UpdateRecommendationResponseDto(success));
        }

    }
}
