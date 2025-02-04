using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.DTOs.Auth;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Controllers
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
        [Route("Logout")]
        public async Task<IActionResult> LogOutUser()
        {
            bool success = await _authService.LogOutUser();
            return Ok(new GatewayLogOutUserResponseDto(success));
        }
    }
}