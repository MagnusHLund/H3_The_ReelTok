using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.auth.Utils;
using reeltok.api.auth.Enums;
using reeltok.api.auth.DTOs;

namespace reeltok.api.auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


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

        [HttpGet]
        [Route("RefreshAccessToken")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            string? refreshToken = CookieUtils.GetCookieValue(HttpContext, TokenName.RefreshToken);

            if(string.IsNullOrEmpty(refreshToken))
            {
                FailureResponseDto failureResponseDto = new FailureResponseDto("No Refresh Token is present!");
                return BadRequest(failureResponseDto);
            }

            AccessToken accessToken = await _authService.RefreshAccessToken(refreshToken).ConfigureAwait(false);

            CookieUtils.AppendTokenToCookie(HttpContext, accessToken, TokenName.AccessToken);

            RefreshTokenResponseDto responseDto = new RefreshTokenResponseDto();

            return Ok(responseDto);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> LogoutUser()
        {
            string? refreshToken = CookieUtils.GetCookieValue(HttpContext, TokenName.RefreshToken);

            if(string.IsNullOrEmpty(refreshToken))
            {
                FailureResponseDto failureResponseDto = new FailureResponseDto("No Refresh Token is present!");
                return BadRequest(failureResponseDto);
            }

            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> GetUserIdByToken()
        {
            string? accessToken = CookieUtils.GetCookieValue(HttpContext, TokenName.AccessToken);

            if(string.IsNullOrEmpty(accessToken))
            {
                FailureResponseDto failureResponseDto = new FailureResponseDto("No Access Token is present!");
                return BadRequest(failureResponseDto);
            }

            throw new NotImplementedException();
        }
    }
}
