using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.Entities;
using reeltok.api.users.ActionFilters;
using reeltok.api.users.DTOs.CreateUser;
using reeltok.api.users.DTOs.UpdateUser;
using reeltok.api.users.DTOs.GetUserById;
using reeltok.api.users.Interfaces.Services;

namespace reeltok.api.users.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // TODO: Refactor DTO names
        // TODO: Call video API to verify video id
        // TODO: Call auth API when creating user
        // TODO: Add Login in User API, using a login controller
        // TODO: Modify profile picture endpoint

        // TODO: CALL AUTH SERVICE TO ADD OTHER USER INFO THERE
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestDto request)
        {
            UserEntity user = await _usersService.CreateUserAsync(request.Username, request.Email, request.Password)
                .ConfigureAwait(false);

            CreateUserResponseDto response = new CreateUserResponseDto(user);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] Guid userId)
        {
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
    }
}
