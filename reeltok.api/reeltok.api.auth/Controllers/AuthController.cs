using reeltok.api.auth.Enums;
using reeltok.api.auth.Utils;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.ActionFilters;
using reeltok.api.auth.DTOs.LoginUser;
using reeltok.api.auth.DTOs.LogoutUser;
using reeltok.api.auth.Interfaces.Services;

namespace reeltok.api.auth.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequestDto request)
        {
            Credentials loginCredentials = new Credentials(request.UserId, request.PlainTextPassword);

            Tokens tokens = await _authService.LoginUserAsync(loginCredentials).ConfigureAwait(false);

            CookieUtils.AppendTokenToCookie(HttpContext, tokens.AccessToken, TokenName.AccessToken);
            CookieUtils.AppendTokenToCookie(HttpContext, tokens.RefreshToken, TokenName.RefreshToken);

            LoginUserResponseDto responseDto = new LoginUserResponseDto();
            return Ok(responseDto);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            string accessToken = CookieUtils.GetCookieValue(HttpContext, TokenName.AccessToken);
            string refreshToken = CookieUtils.GetCookieValue(HttpContext, TokenName.RefreshToken);

            await _authService.LogoutUserAsync(accessToken, refreshToken).ConfigureAwait(false);

            CookieUtils.DeleteCookie(HttpContext, TokenName.AccessToken);
            CookieUtils.DeleteCookie(HttpContext, TokenName.RefreshToken);

            LogoutUserResponseDto responseDto = new LogoutUserResponseDto();
            return Ok(responseDto);
        }
    }
}
