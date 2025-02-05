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
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] GatewayLoginRequestDto request)
        {
            UserProfileData userProfileData = await _usersService.LoginUser(request.Email, request.Password);
            GatewayLoginResponseDto responseDto = UserMapper.ConvertUserProfileDataToResponseDto<GatewayLoginResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser([FromBody] GatewayCreateUserRequestDto request)
        {
            UserProfileData userProfileData = await _usersService.CreateUser(request.Email, request.Username, request.Password);
            GatewayCreateUserResponseDto responseDto = UserMapper.ConvertUserProfileDataToResponseDto<GatewayCreateUserResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetUserProfileData/{userId}")]
        public async Task<IActionResult> GetUserProfileData([FromRoute] Guid userId)
        {
            UserProfileData userProfileData = await _usersService.GetUserProfileData(userId);
            GatewayGetUserProfileDataResponseDto responseDto = UserMapper.ConvertUserProfileDataToResponseDto<GatewayGetUserProfileDataResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPut]
        [Route("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] GatewayUpdateUserDetailsRequestDto request)
        {
            EditableUserDetails userProfileData = await _usersService.UpdateUserDetails(request.Username, request.Email);
            GatewayUpdateUserDetailsResponseDto responseDto = UserMapper.ConvertEditableUserDetailsToDto<GatewayUpdateUserDetailsResponseDto>(userProfileData);

            return Ok(responseDto);
        }

        [HttpPut]
        [Route("UpdateProfilePicture")]
        public async Task<IActionResult> UpdateProfilePicture([FromBody] GatewayUpdateProfilePictureRequestDto request)
        {
            string profilePictureUrl = await _usersService.UpdateProfilePicture(request.ProfilePicture);
            GatewayUpdateProfilePictureResponseDto responseDto = new GatewayUpdateProfilePictureResponseDto(profilePictureUrl);

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetAllSubscriptionsForUser")]
        public async Task<IActionResult> GetAllSubscriptionsForUser([FromRoute] Guid userId)
        {
            List<UserDetails> users = await _usersService.GetAllSubscriptionsForUser(userId);
            GatewayGetAllSubscriptionsForUserResponseDto responseDto = new GatewayGetAllSubscriptionsForUserResponseDto(users);

            if (users.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetAllSubscribingToUser")]
        public async Task<IActionResult> GetAllSubscribingToUser([FromBody] Guid userId)
        {
            List<UserDetails> users = await _usersService.GetAllSubscribingToUser(userId);
            GatewayGetAllSubscribingToUserResponseDto responseDto = new GatewayGetAllSubscribingToUserResponseDto(users);

            if (users.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(responseDto);
        }
    }
}