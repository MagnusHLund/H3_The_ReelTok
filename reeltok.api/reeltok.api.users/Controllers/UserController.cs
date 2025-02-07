using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.DTOs.UserResponseDTO;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces;
using reeltok.api.users.Mappers;

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

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserProfileData user)
        {
            UserProfileData dbUser = await _usersService.CreateUserAsync(user);

            // Map the entity to DTO
            ReturnCreateUserResponseDTO responseDto = UserMapper.ToReturnCreateUserResponseDTO(dbUser);

            return Ok(responseDto);
        }
    }
}