using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] GatewayLoginRequestDto request)
        {
            UserProfileData userProfileData = await _usersService.LoginUser(request.Email, request.Password).ConfigureAwait(false);
            GatewayLoginResponseDto responseDto = UserMapper.ConvertUserProfileDataToResponseDto<GatewayLoginResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] GatewayCreateUserRequestDto request)
        {
            UserProfileData userProfileData = await _usersService.CreateUser(request.Email, request.Username, request.Password).ConfigureAwait(false);
            GatewayCreateUserResponseDto responseDto = UserMapper.ConvertUserProfileDataToResponseDto<GatewayCreateUserResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserProfileData([FromRoute] Guid userId)
        {
            UserProfileData userProfileData = await _usersService.GetUserProfileData(userId).ConfigureAwait(false);
            GatewayGetUserProfileDataResponseDto responseDto = UserMapper.ConvertUserProfileDataToResponseDto<GatewayGetUserProfileDataResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserDetails([FromBody] GatewayUpdateUserDetailsRequestDto request)
        {
            EditableUserDetails userProfileData = await _usersService.UpdateUserDetails(request.Username, request.Email).ConfigureAwait(false);
            GatewayUpdateUserDetailsResponseDto responseDto = UserMapper.ConvertEditableUserDetailsToDto<GatewayUpdateUserDetailsResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPut]
        [Route("profile-picture")]
        public async Task<IActionResult> UpdateProfilePicture([FromBody] GatewayUpdateProfilePictureRequestDto request)
        {
            string profilePictureUrl = await _usersService.UpdateProfilePicture(request.ProfilePicture).ConfigureAwait(false);
            GatewayUpdateProfilePictureResponseDto responseDto = new GatewayUpdateProfilePictureResponseDto(profilePictureUrl);

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("{userId}/subscriptions")]
        public async Task<IActionResult> GetAllSubscriptionsForUser([FromRoute] Guid userId)
        {
            List<UserDetails> users = await _usersService.GetAllSubscriptionsForUser(userId).ConfigureAwait(false);
            GatewayGetAllSubscriptionsForUserResponseDto responseDto = new GatewayGetAllSubscriptionsForUserResponseDto(users);

            if (users.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("{userId}/subscribers")]
        public async Task<IActionResult> GetAllSubscribingToUser([FromRoute] Guid userId)
        {
            List<UserDetails> users = await _usersService.GetAllSubscribingToUser(userId).ConfigureAwait(false);
            GatewayGetAllSubscribingToUserResponseDto responseDto = new GatewayGetAllSubscribingToUserResponseDto(users);

            if (users.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(responseDto);
        }
    }
}
