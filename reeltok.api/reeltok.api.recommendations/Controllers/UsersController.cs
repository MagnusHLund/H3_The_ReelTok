using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ActionFilters;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.DTOs.GetUserInterest;
using reeltok.api.recommendations.DTOs.AddUserInterest;
using reeltok.api.recommendations.DTOs.UpdateUserInterest;

namespace reeltok.api.recommendations.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class UserRecommendationsController : ControllerBase
    {
        private readonly IUsersService _userRecommendationService;

        public UserRecommendationsController(IUsersService userRecommendationService)
        {
            _userRecommendationService = userRecommendationService;
        }

        // Called from Users api
        [HttpGet]
        public async Task<IActionResult> GetUserInterestAsync([FromQuery] Guid userId)
        {
            UserInterestEntity userInterest = await _userRecommendationService.GetUserInterestAsync(userId).ConfigureAwait(false);

            CategoryEntity category = userInterest.Categories.First();

            GetUserInterestResponseDto response = new GetUserInterestResponseDto();
            return Ok(response);
        }

        // Called from Users api
        [HttpPost]
        public async Task<IActionResult> AddUserInterestAsync([FromBody] AddUserInterestRequestDto request)
        {
            UserInterestDetails userInterestDetails = UserRecommendationMapper.ToUserInterestDetailsFromDTO(request);

            UserInterestEntity userInterest = new UserInterestEntity(userInterestDetails);

            bool isAdded = await _userRecommendationService.AddRecommendationForUserAsync(userInterest, request.CategoryId);

            AddUserInterestResponseDto response = new AddUserInterestResponseDto();
            return Ok(isAdded);
        }

        // Called from Users api
        [HttpPut]
        public async Task<IActionResult> UpdateUserInterestAsync(UpdateUserInterestRequestDto request)
        {
            bool isUpdated = await _userRecommendationService.UpdateRecommendationForUserAsync
                (request.UserId, request.OldCategoryId, request.NewCategoryId);

            if (!isUpdated)
            {
                return BadRequest(new FailureResponseDto("Failed to update user recommendation"));
            }

            UpdateUserInterestResponseDto response = new UpdateUserInterestResponseDto();
            return Ok(response);
        }
    }
}
