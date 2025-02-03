using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto request)
        {
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
        {
        }

        [HttpGet]
        [Route("GetUserProfileData/{userId}")] // TODO: Verify this
        public async Task<IActionResult> GetUserProfileData([FromBody] LoginRequestDto request) // TODO: Replace parameters
        {
        }

        [HttpPut]
        [Route("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsRequestDto request)
        {
        }

        [HttpPut]
        [Route("UpdateProfilePicture")]
        public async Task<IActionResult> UpdateProfilePicture([FromBody] UpdateProfilePictureRequestDto request)
        {
        }

        [HttpGet]
        [Route("GetAllSubscriptionsForUser")]
        public async Task<IActionResult> GetAllSubscriptionsForUser([FromBody] GetAllSubscriptionsForUserRequestDto request)
        {
        }

        [HttpGet]
        [Route("GetAllSubscribingToUser")]
        public async Task<IActionResult> GetAllSubscribingToUser([FromBody] GetAllSubscribingToUserRequestDto request)
        {
        }
    }
}