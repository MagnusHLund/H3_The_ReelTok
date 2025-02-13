using reeltok.api.auth.DTOs;
using reeltok.api.auth.Enums;
using reeltok.api.auth.Utils;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.ActionFilters;

namespace reeltok.api.auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/xml")]
    [Produces("application/xml")]
    [ValidateModel]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("GetUserIdByToken")]
        public IActionResult GetUserIdByToken()
        {
            string? accessTokenValue = CookieUtils.GetCookieValue(HttpContext, TokenName.AccessToken);

            if (string.IsNullOrEmpty(accessTokenValue))
            {
                FailureResponseDto failureResponseDto = new FailureResponseDto("No Access Token is present!");
                return BadRequest(failureResponseDto);
            }

            Guid userId = _authService.GetUserIdByToken(accessTokenValue);
            GetUserIdByTokenResponseDto responseDto = new GetUserIdByTokenResponseDto(userId);

            return Ok(responseDto);
        }

        // TODO: SQL to ensure tokens are invalidated, if expired.
        // TODO: Ensure that tokens are revoked correctly, I think they are, but just make sure
        // TODO: Ensure that DTOs have the correct xml attributes
        // TODO: Optimize database (table lengths, and such)
        // TODO: Ensure that all token usage checks if the token is expired!
        // TODO: Create factory for tests
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
        {
            CreateDetails CreateDetails = new CreateDetails(request.UserId, request.PlainTextPassword);

            Tokens tokens = await _authService.CreateUser(CreateDetails).ConfigureAwait(false);

            CookieUtils.AppendTokenToCookie(HttpContext, tokens.AccessToken, TokenName.AccessToken);
            CookieUtils.AppendTokenToCookie(HttpContext, tokens.RefreshToken, TokenName.RefreshToken);

            CreateUserResponseDto responseDto = new CreateUserResponseDto();

            return Ok(responseDto);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequestDto request)
        {
            LoginCredentials loginCredentials = new LoginCredentials(request.UserId, request.PlainTextPassword);

            Tokens tokens = await _authService.LoginUser(loginCredentials).ConfigureAwait(false);

            CookieUtils.AppendTokenToCookie(HttpContext, tokens.AccessToken, TokenName.AccessToken);
            CookieUtils.AppendTokenToCookie(HttpContext, tokens.RefreshToken, TokenName.RefreshToken);

            LoginUserResponseDto responseDto = new LoginUserResponseDto();

            return Ok(responseDto);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> LogoutUser()
        {
            string accessToken = CookieUtils.GetCookieValue(HttpContext, TokenName.AccessToken);
            string refreshToken = CookieUtils.GetCookieValue(HttpContext, TokenName.RefreshToken);

            await _authService.LogoutUser(accessToken, refreshToken).ConfigureAwait(false);

            CookieUtils.DeleteCookie(HttpContext, TokenName.AccessToken);
            CookieUtils.DeleteCookie(HttpContext, TokenName.RefreshToken);

            LogoutUserResponseDto responseDto = new LogoutUserResponseDto();
            return Ok(responseDto);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            string accessToken = CookieUtils.GetCookieValue(HttpContext, TokenName.AccessToken);

            Guid userId = _authService.GetUserIdByToken(accessToken);
            await _authService.DeleteUser(userId).ConfigureAwait(false);

            DeleteUserResponseDto responseDto = new DeleteUserResponseDto();
            return Ok(responseDto);
        }
    }
}
