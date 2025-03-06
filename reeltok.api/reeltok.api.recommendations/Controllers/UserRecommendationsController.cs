using reeltok.api.auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Mappers;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ValueObjects;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRecommendationsController : ControllerBase
    {
        private readonly IUserRecommendationService _userRecommendationService;

        public UserRecommendationsController(IUserRecommendationService userRecommendationService)
        {
            _userRecommendationService = userRecommendationService;
        }

        [HttpPost("Add user recommendation")]
        public async Task<IActionResult> AddUserRecommendationAsync([FromBody] CreateUserInterestDto dto)
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

        [HttpGet("Get user recommendation")]
        public async Task<IActionResult> GetUserRecommendationAsync([FromQuery] Guid userId)
        {
            UserInterestEntity? userInterest = await _userRecommendationService.GetUserInterestAsync(userId);

            if (userInterest == null)
            {
                return BadRequest(new FailureResponseDto("User not found"));
            }

            if (userInterest.Categories == null || !userInterest.Categories.Any())
            {
                return BadRequest(new FailureResponseDto("Category not found"));
            }

            CategoryEntity category = userInterest.Categories.First();

            UserInterestResponseDto userInterestDto = UserRecommendationMapper.ToUserInterestDTOFromEntity(userInterest, category);

            return Ok(userInterestDto);
        }

        [HttpPut("Update user recommendation")]
        public async Task<IActionResult> UpdateUserRecommendationAsync(UpdateUserInterestDto dto)
        {
            bool isUpdated = await _userRecommendationService.UpdateRecommendationForUserAsync
                (dto.UserId, dto.OldCategoryId, dto.NewCategoryId);

            if (!isUpdated)
            {
                return BadRequest(new FailureResponseDto("Failed to update user recommendation"));
            }

            return Ok(isUpdated);
        }
    }
}
