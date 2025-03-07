using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.Entities;
using reeltok.api.users.ActionFilters;
using reeltok.api.users.DTOs.CreateUser;
using reeltok.api.users.DTOs.UpdateUser;
using reeltok.api.users.DTOs.GetUserById;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.DTOs.UpdateProfilePicture;

namespace reeltok.api.users.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestDto request)
        {
            UserEntity user = await _usersService.CreateUserAsync(request.Username, request.Email, request.Password, request.Interests)
                .ConfigureAwait(false);

            CreateUserResponseDto response = new CreateUserResponseDto(user);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] Guid userId)
        {
            // TODO: Also return total amount of subscribers and total amount of subscriptions

            UserEntity user = await _usersService.GetUserByIdAsync(userId).ConfigureAwait(false);

            GetUserByIdResponseDto response = new GetUserByIdResponseDto(user);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequestDto request)
        {
            UserEntity updatedUser = await _usersService.UpdateUserAsync(request.UserId, request.Username, request.Email)
                .ConfigureAwait(false);

            UpdateUserResponseDto response = new UpdateUserResponseDto(updatedUser);
            return Ok(response);
        }

        [HttpPut("profile-picture")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateUserProfilePictureAsync([FromForm] UpdateUserProfilePictureRequestDto request)
        {
            UserEntity updatedUser = await _usersService.UpdateUserProfilePictureAsync(request.ProfilePicture, request.UserId).ConfigureAwait(false);

            UpdateUserProfilePictureResponseDto response = new UpdateUserProfilePictureResponseDto(updatedUser);
            return Ok(response);
        }
    }
}
