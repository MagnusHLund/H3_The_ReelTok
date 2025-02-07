using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces;

namespace reeltok.api.users.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("{Create User}")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserProfileData user)
        {
            try
            {
                await _usersService.CreateUserAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}