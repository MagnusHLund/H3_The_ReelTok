using Microsoft.AspNetCore.Mvc;
using reeltok.api.auth.DTOs;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Mappers;
using reeltok.api.recommendations.ValueObjects;

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

        [HttpPost("Add user recommendation")]
        public async Task<IActionResult> AddUserRecommendationAsync([FromBody] CreateUserInterestDTO dto)
        {
            UserInterestDetails userInterestDetails = UserRecommendationMapper.ToUserInterestDetailsFromDTO(dto);

            UserInterestEntity userInterest = new UserInterestEntity(userInterestDetails);

            bool isAdded = await _userRecommendationService.AddRecommendationForUserAsync(userInterest, dto.CategoryId);

            if (!isAdded)
            {
                return BadRequest(new FailureResponseDto("Failed to add user recommendation"));
            }

            return Ok(isAdded);
        }
    }
}
