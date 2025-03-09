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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userRecommendationService;

        public UsersController(IUsersService userRecommendationService)
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
            CategoryType userInterest = await _userRecommendationService
                .AddInterestForUserAsync(request.UserId, request.Interest).ConfigureAwait(false);

            AddUserInterestResponseDto response = new AddUserInterestResponseDto(userInterest);
            return Ok(response);
        }

        // Called from Users api
        [HttpPut]
        public async Task<IActionResult> UpdateUserInterestAsync(UpdateUserInterestRequestDto request)
        {
            CategoryType interest = await _userRecommendationService.UpdateInterestForUserAsync(request.UserId, request.Interest)
                .ConfigureAwait(false);

            UpdateUserInterestResponseDto response = new UpdateUserInterestResponseDto(interest);
            return Ok(response);
        }
    }
}
