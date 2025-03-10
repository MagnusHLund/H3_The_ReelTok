using reeltok.api.auth.DTOs;
using reeltok.api.auth.Enums;
using reeltok.api.auth.Utils;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.auth.DTOs.SignUp;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.ActionFilters;
using reeltok.api.auth.DTOs.DeleteUser;
using reeltok.api.auth.Interfaces.Services;
using reeltok.api.auth.DTOs.GetUserIdByToken;

namespace reeltok.api.auth.Controllers
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

        [HttpGet]
        public IActionResult GetUserIdByAuthenticationTokenAsync()
        {
            string? accessTokenValue = CookieUtils.GetCookieValue(HttpContext, TokenName.AccessToken);

            Guid userId = _usersService.GetUserIdByAccessTokenAsync(accessTokenValue);

            GetUserIdByTokenResponseDto responseDto = new GetUserIdByTokenResponseDto(userId);
            return Ok(responseDto);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequestDto request)
        {
            Credentials userCredentials = new Credentials(request.UserId, request.PlainTextPassword);

            Tokens tokens = await _usersService.SignUpAsync(userCredentials).ConfigureAwait(false);

            CookieUtils.AppendTokenToCookie(HttpContext, tokens.AccessToken, TokenName.AccessToken);
            CookieUtils.AppendTokenToCookie(HttpContext, tokens.RefreshToken, TokenName.RefreshToken);

            SignUpResponseDto response = new SignUpResponseDto();
            return Ok(response);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            string accessToken = CookieUtils.GetCookieValue(HttpContext, TokenName.AccessToken);

            Guid userId = _usersService.GetUserIdByAccessTokenAsync(accessToken);
            await _usersService.DeleteUser(userId).ConfigureAwait(false);

            CookieUtils.DeleteCookie(HttpContext, TokenName.AccessToken);
            CookieUtils.DeleteCookie(HttpContext, TokenName.RefreshToken);

            DeleteUserResponseDto responseDto = new DeleteUserResponseDto();
            return Ok(responseDto);
        }
    }
}
