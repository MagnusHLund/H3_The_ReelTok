using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Enums;
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
            CategoryType userInterest = await _userRecommendationService.GetUserInterestAsync(userId).ConfigureAwait(false);

            GetUserInterestResponseDto response = new GetUserInterestResponseDto(userInterest);
            return Ok(response);
        }

        // Called from Users api
        [HttpPost]
        public async Task<IActionResult> AddUserInterestAsync([FromBody] AddUserInterestRequestDto request)
        {
            CategoryType interest = await _userRecommendationService
                .AddInterestForUserAsync(request.UserId, request.Interest).ConfigureAwait(false);

            AddUserInterestResponseDto response = new AddUserInterestResponseDto(interest);
            return Ok(response);
        }

        // Called from Users api
        [HttpPut]
        public async Task<IActionResult> UpdateUserInterestAsync(UpdateUserInterestRequestDto request)
        {
            bool isUpdated = await _userRecommendationService
            .UpdateInterestForUserAsync(request.UserId, request.OldCategoryId, request.NewCategoryId).ConfigureAwait(false);

            if (!isUpdated)
            {
                return BadRequest(new FailureResponseDto("Failed to update user recommendation"));
            }

            UpdateUserInterestResponseDto response = new UpdateUserInterestResponseDto();
            return Ok(response);
        }
    }
}
