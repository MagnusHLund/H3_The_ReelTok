using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.DTOs.Auth.LogOutUser;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        // TODO: Overall in this api, reduce complexity of generics, if possible
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogOutUserAsync()
        {
            bool success = await _authService.LogOutUser().ConfigureAwait(false);
            GatewayLogOutUserResponseDto responseDto = new GatewayLogOutUserResponseDto(success);

            return Ok(responseDto);
        }
    }
}
