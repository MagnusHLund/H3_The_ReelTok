using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.Entities.Users;
using reeltok.api.gateway.DTOs.Users.Login;
using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.DTOs.Users.CreateUser;
using reeltok.api.gateway.DTOs.Users.UpdateUserDetails;
using reeltok.api.gateway.DTOs.Users.GetUserProfileData;
using reeltok.api.gateway.DTOs.Users.UpdateProfilePicture;
using reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser;
using reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser;
using reeltok.api.gateway.DTOs.Users.SubscribeToUser;
using reeltok.api.gateway.DTOs.Users.UnsubscribeToUser;

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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
        {
            ExternalUserEntity user = await _usersService.GetUserByIdAsync(userId).ConfigureAwait(false);

            GatewayGetUserByIdResponseDto responseDto = new GatewayGetUserByIdResponseDto(user);
            return Ok(responseDto);
        }

        [HttpGet("{userId}/subscriptions")]
        public async Task<IActionResult> GetUserSubscriptionsAsync(
            [FromRoute] Guid userId,
            [FromQuery] int pageNumber,
            [FromQuery, Range(1, byte.MaxValue)] byte pageSize = 15
        )
        {
            List<ExternalUserEntity> users = await _usersService.GetUserSubscriptionsAsync(userId, pageNumber, pageSize)
                .ConfigureAwait(false);

            GatewayGetAllSubscriptionsForUserResponseDto responseDto = new GatewayGetAllSubscriptionsForUserResponseDto(users);
            return Ok(responseDto);
        }

        [HttpGet("{userId}/subscribers")]
        public async Task<IActionResult> GetUserSubscribersAsync(
            [FromRoute] Guid userId,
            [FromQuery] int pageNumber,
            [FromQuery, Range(1, byte.MaxValue)] byte pageSize = 15
        )
        {
            List<ExternalUserEntity> users = await _usersService.GetUserSubscribersAsync(userId, pageNumber, pageSize)
                .ConfigureAwait(false);

            GatewayGetAllSubscribingToUserResponseDto responseDto = new GatewayGetAllSubscribingToUserResponseDto(users);
            return Ok(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] GatewayCreateUserRequestDto request)
        {
            UserEntity createdUser = await _usersService
                .CreateUserAsync(request.Email, request.Username, request.Password, request.Interest)
                .ConfigureAwait(false);

            GatewayCreateUserResponseDto responseDto = new GatewayCreateUserResponseDto(createdUser);
            return Ok(responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] GatewayLoginRequestDto request)
        {
            UserEntity user = await _usersService.LoginUserAsync(request.Email, request.Password)
                .ConfigureAwait(false);

            GatewayLoginResponseDto responseDto = new GatewayLoginResponseDto(user);
            return Ok(responseDto);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeToUserAsync([FromBody] GatewaySubscribeToUserRequestDto request)
        {
            bool success = await _usersService.SubscribeToUserAsync(request.SubscribingToUserId).ConfigureAwait(false);

            GatewaySubscribeToUserResponseDto responseDto = new GatewaySubscribeToUserResponseDto(success);
            return Ok(responseDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserDetailsAsync([FromBody] GatewayUpdateUserDetailsRequestDto request)
        {
            UserEntity user = await _usersService
                .UpdateUserDetailsAsync(request.Username, request.Email, request.Interest)
                .ConfigureAwait(false);

            GatewayUpdateUserDetailsResponseDto responseDto = new GatewayUpdateUserDetailsResponseDto(user);
            return Ok(responseDto);
        }

        [HttpPut("profile-picture")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfilePictureAsync([FromBody] GatewayUpdateProfilePictureRequestDto request)
        {
            UserEntity user = await _usersService.UpdateProfilePictureAsync(request.ProfilePicture)
                .ConfigureAwait(false);

            GatewayUpdateProfilePictureResponseDto responseDto = new GatewayUpdateProfilePictureResponseDto(user);
            return Ok(responseDto);
        }

        [HttpDelete("unsubscribe")]
        public async Task<IActionResult> UnsubscribeToUserAsync([FromQuery, JsonProperty("UserId")] Guid unsubscribingToUserId)
        {
            bool success = await _usersService.UnsubscribeToUserAsync(unsubscribingToUserId).ConfigureAwait(false);

            GatewayUnsubscribeToUserResponseDto responseDto = new GatewayUnsubscribeToUserResponseDto(success);
            return Ok(responseDto);
        }
    }
}
