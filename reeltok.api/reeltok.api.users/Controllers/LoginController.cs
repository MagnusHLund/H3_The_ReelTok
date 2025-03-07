using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.Entities;
using reeltok.api.users.DTOs.Login;
using reeltok.api.users.ActionFilters;
using reeltok.api.users.Interfaces.Services;

namespace reeltok.api.users.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequestDto request)
        {
            UserEntity user = await _loginService.LoginUserAsync(request.Email, request.Password)
                .ConfigureAwait(false);

            LoginResponseDto response = new LoginResponseDto(user);
            return Ok(response);
        }
    }
}