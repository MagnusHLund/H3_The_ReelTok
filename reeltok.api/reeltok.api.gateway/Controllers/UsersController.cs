using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.Entities;
using Microsoft.IdentityModel.Tokens;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.DTOs.Users.Login;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.DTOs.Users.CreateUser;
using reeltok.api.gateway.DTOs.Users.UpdateUserDetails;
using reeltok.api.gateway.DTOs.Users.GetUserProfileData;
using reeltok.api.gateway.DTOs.Users.UpdateProfilePicture;
using reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser;
using reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("{userId}/subscriptions")]
        public async Task<IActionResult> GetAllSubscriptionsForUserAsync([FromRoute] Guid userId)
        {
            List<UserDetails> users = await _usersService.GetAllSubscriptionsForUser(userId).ConfigureAwait(false);
            GatewayGetAllSubscriptionsForUserResponseDto responseDto = new GatewayGetAllSubscriptionsForUserResponseDto(users);

            if (users.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(responseDto);
        }

        [HttpGet("{userId}/subscribers")]
        public async Task<IActionResult> GetAllSubscribingToUserAsync([FromRoute] Guid userId)
        {
            List<UserDetails> users = await _usersService.GetAllSubscribingToUser(userId).ConfigureAwait(false);
            GatewayGetAllSubscribingToUserResponseDto responseDto = new GatewayGetAllSubscribingToUserResponseDto(users);

            if (users.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] GatewayCreateUserRequestDto request)
        {
            UserProfileData userProfileData = await _usersService.CreateUser(request.Email, request.Username, request.Password).ConfigureAwait(false);
            GatewayCreateUserResponseDto responseDto = UserMapper.ConvertUserProfileDataToResponseDto<GatewayCreateUserResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] GatewayLoginRequestDto request)
        {
            UserProfileData userProfileData = await _usersService.LoginUser(request.Email, request.Password).ConfigureAwait(false);
            GatewayLoginResponseDto responseDto = UserMapper.ConvertUserProfileDataToResponseDto<GatewayLoginResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> GetUserProfileDataAsync([FromRoute] Guid userId)
        {
            UserProfileData userProfileData = await _usersService.GetUserProfileData(userId).ConfigureAwait(false);
            GatewayGetUserByIdResponseDto responseDto = UserMapper.ConvertUserProfileDataToResponseDto<GatewayGetUserByIdResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserDetailsAsync([FromBody] GatewayUpdateUserDetailsRequestDto request)
        {
            EditableUserDetails userProfileData = await _usersService.UpdateUserDetails(request.Username, request.Email)
                .ConfigureAwait(false);

            GatewayUpdateUserDetailsResponseDto responseDto = UserMapper.
                ConvertEditableUserDetailsToDto<GatewayUpdateUserDetailsResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPut("profile-picture")]
        public async Task<IActionResult> UpdateProfilePictureAsync([FromBody] GatewayUpdateProfilePictureRequestDto request)
        {
            string profilePictureUrl = await _usersService.UpdateProfilePicture(request.ProfilePicture).ConfigureAwait(false);

            GatewayUpdateProfilePictureResponseDto responseDto = new GatewayUpdateProfilePictureResponseDto(profilePictureUrl);
            return Ok(responseDto);
        }
    }
}
