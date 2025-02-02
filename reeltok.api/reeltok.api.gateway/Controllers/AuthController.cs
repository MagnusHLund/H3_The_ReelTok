using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.DTOs.Auth;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOutUser()
        {
            bool success = await _authService.LogOutUser();
            return Ok(new LogOutUserResponseDto(success));
        }

        //! I forgot that we don't need this, but now I'll commit it incase we do anyway.
        /* 
        [HttpGet("getUserIdByToken")]
        public async Task<IActionResult> GetUserIdByToken()
        {
            Guid userId = await _authService.GetUserIdByToken();

            bool success = true;
            return Ok(new GetUserIdByTokenResponseDto(success, userId));
        } 
        */
    }
}