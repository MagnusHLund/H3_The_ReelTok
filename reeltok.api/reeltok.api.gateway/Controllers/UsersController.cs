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
        public async Task<IActionResult> LoginUser([FromBody] GatewayLoginRequestDto request)
        {
            return (Ok(request));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser([FromBody] GatewayCreateUserRequestDto request)
        {
            return (Ok(request));
        }

        [HttpGet]
        [Route("GetUserProfileData/{userId}")] // TODO: Verify this
        public async Task<IActionResult> GetUserProfileData([FromBody] GatewayLoginRequestDto request) // TODO: Replace parameters
        {
            return (Ok(request));
        }

        [HttpPut]
        [Route("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] GatewayUpdateUserDetailsRequestDto request)
        {
            return (Ok(request));
        }

        [HttpPut]
        [Route("UpdateProfilePicture")]
        public async Task<IActionResult> UpdateProfilePicture([FromBody] GatewayUpdateProfilePictureRequestDto request)
        {
            return (Ok(request));
        }

        [HttpGet]
        [Route("GetAllSubscriptionsForUser")]
        public async Task<IActionResult> GetAllSubscriptionsForUser([FromBody] GatewayGetAllSubscriptionsForUserRequestDto request)
        {
            return (Ok(request));
        }

        [HttpGet]
        [Route("GetAllSubscribingToUser")]
        public async Task<IActionResult> GetAllSubscribingToUser([FromBody] GatewayGetAllSubscribingToUserRequestDto request)
        {
            return (Ok(request));
        }
    }
}