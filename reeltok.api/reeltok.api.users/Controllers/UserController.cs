using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.DTOs.UserRequests;
using reeltok.api.users.DTOs.UserResponses;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Mappers;
using reeltok.api.users.ValueObjects;

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

        [HttpPost("Create A User")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                return BadRequest("User cannot be null");
            }

            User userModel = user.ToUsersFromCreateDTO();

            // Adding the leftover Properties
            userModel.UserId = Guid.NewGuid();

            User dbUser = await _usersService.CreateUserAsync(userModel).ConfigureAwait(false);
            // Map the entity to DTO
            ReturnCreateUserResponseDTO responseDto = UserMapper.ToReturnCreateUserResponseDTO(dbUser);

            return Ok(responseDto);
        }

        [HttpGet("Get User By Id")]
        public async Task<IActionResult> GetUserById([FromQuery] Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userId == Guid.Empty)
            {
                return BadRequest("User Id cannot be empty");
            }

            User? user = await _usersService.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                return NotFound("User not found");
            }

            ReturnCreateUserResponseDTO responseDto = UserMapper.ToReturnCreateUserResponseDTO(user);

            return Ok(responseDto);
        }

        [HttpPut("Update User")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequestDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                return BadRequest("User cannot be null");
            }

            User? existingUser = await _usersService.GetUserByIdAsync(user.UserId).ConfigureAwait(false);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            UserDetails? updatedUserDetails = user.ToUserDetailsFromUpdateDTO(existingUser);
            existingUser.UserDetails = updatedUserDetails;

            User? dbUser = await _usersService.UpdateUserAsync(existingUser, user.UserId).ConfigureAwait(false);

            if (dbUser == null)
            {
                return NotFound("User not found");
            }

            ReturnCreateUserResponseDTO responseDto = UserMapper.ToReturnCreateUserResponseDTO(dbUser);

            return Ok(responseDto);
        }

        [HttpDelete("Delete User")]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userId == Guid.Empty)
            {
                return BadRequest("User Id cannot be empty");
            }

            User? user = await _usersService.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                return NotFound("User not found");
            }

            bool isDeleted = await _usersService.DeleteUserAsync(userId).ConfigureAwait(false);

            return Ok(isDeleted);
        }
    }
}