using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.ActionFilters;
using reeltok.api.users.DTOs.Login;

namespace reeltok.api.users.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        // TODO: Add login functionality

        public LoginController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
        {
            LoginResponseDto response = new LoginResponseDto();
            return Ok(response);
        }
    }
}