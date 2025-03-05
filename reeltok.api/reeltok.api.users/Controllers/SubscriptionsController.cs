using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.Mappers;
using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.ActionFilters;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.DTOs.SubscriptionRequests;
using reeltok.api.users.DTOs.SubscriptionResponses;

namespace reeltok.api.users.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ISubscriptionsService _subscriptionService;

        public SubscriptionsController(ISubscriptionsService subscriptionService, IUsersService usersService)
        {
            _usersService = usersService;
            _subscriptionService = subscriptionService;
        }

        // TODO: Check that both user id are not the same
        [HttpPost("Follow A User")]
        public async Task<IActionResult> CreateSubscriptionAsync([FromBody] SubscribeRequestDto subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (subscription == null)
            {
                return BadRequest("Subscription cannot be null");
            }

            User? existingUser = await _usersService.GetUserByIdAsync(subscription.UserId).ConfigureAwait(false);

            if (existingUser == null)
            {
                return BadRequest("Follower User Id does not exist.");
            }

            User? existingUser2 = await _usersService.GetUserByIdAsync(subscription.SubscribingToUserId).ConfigureAwait(false);

            if (existingUser2 == null)
            {
                return BadRequest("Following User Id does not exist.");
            }

            SubscribptionDetails details = subscription.ToSubscriptionFromCreateDTO();

            Subscription subscriptionModel = new Subscription(details);

            bool isSubscribed = await _subscriptionService.SubscribeAsync(subscriptionModel).ConfigureAwait(false);

            if (isSubscribed)
            {
                return Ok("User followed successfully!");
            }

            return StatusCode(500, "Failed to follow user.");
        }


        [HttpDelete("Unfollow A User")]
        public async Task<IActionResult> UnsubscribeAsync([FromBody] UnsubscribeRequestDto unSubscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (unSubscription == null)
            {
                return BadRequest("Subscription cannot be null");
            }

            User? existingUser = await _usersService.GetUserByIdAsync(unSubscription.SubscriberUserId).ConfigureAwait(false);

            if (existingUser == null)
            {
                return BadRequest("Follower User Id does not exist.");
            }

            User? existingUser2 = await _usersService.GetUserByIdAsync(unSubscription.SubscribingToUserId).ConfigureAwait(false);

            if (existingUser2 == null)
            {
                return BadRequest("Following User Id does not exist.");
            }

            bool isUnsubscribed = await _subscriptionService.UnsubscribeAsync(unSubscription.SubscriberUserId, unSubscription.SubscribingToUserId).ConfigureAwait(false);

            if (isUnsubscribed)
            {
                return Ok("User unfollowed successfully!");
            }

            return StatusCode(500, "Failed to unfollow user.");
        }


        [HttpGet("User Followers")]
        public async Task<IActionResult> GetAllSubscribersAsync([FromQuery] Guid request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request == Guid.Empty) // Check for empty GUID
            {
                return BadRequest("Request cannot be null or empty");
            }

            // Check if the user exists
            User? existingUser = await _usersService.GetUserByIdAsync(request).ConfigureAwait(false);
            if (existingUser == null)
            {
                return BadRequest("User Id does not exist.");
            }

            // Get list of subscriber IDs
            List<Guid> subscriberIds = await _subscriptionService.GetAllSubscribersIdAsync(request).ConfigureAwait(false);
            if (subscriberIds == null || subscriberIds.Count == 0)
            {
                return NotFound("No subscriptions found.");
            }

            // Fetch users one by one to avoid DbContext concurrency issues
            List<User> users = new();
            foreach (Guid id in subscriberIds)
            {
                User? user = await _usersService.GetUserByIdAsync(id).ConfigureAwait(false);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            // Map users to DTOs
            List<UserDetails> userDetailsList = users
                .Select(user =>
                {
                    DTOs.UserResponses.ReturnCreateUserResponseDTO userDetailsDto = user.ToReturnCreateUserResponseDTO();
                    return new UserDetails(
                        userName: userDetailsDto.UserName,
                        profileUrl: userDetailsDto.ProfileUrl,
                        profilePictureUrl: userDetailsDto.ProfilePictureUrl,
                        new HiddenUserDetails(email: userDetailsDto.Email)
                    );
                })
                .ToList();

            ServiceGetAllSubscribingToUserResponseDto responseDto = new ServiceGetAllSubscribingToUserResponseDto(userDetailsList);
            return Ok(responseDto);
        }


        [HttpGet("User Following")]
        public async Task<IActionResult> GetAllSubscriptionsAsync([FromQuery] Guid request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request == Guid.Empty) // Fix: Use Guid.Empty instead of null check
            {
                return BadRequest("Request cannot be null or empty");
            }

            // Check if the user exists
            User? existingUser = await _usersService.GetUserByIdAsync(request).ConfigureAwait(false);
            if (existingUser == null)
            {
                return BadRequest("User Id does not exist.");
            }

            // Get list of subscribing to IDs
            List<Guid> subscribingToIds = await _subscriptionService.GetAllSubscriptionIdAsync(request).ConfigureAwait(false);
            if (subscribingToIds == null || subscribingToIds.Count == 0)
            {
                return NotFound("No subscriptions found.");
            }

            List<User> users = new();
            foreach (Guid id in subscribingToIds)
            {
                User? user = await _usersService.GetUserByIdAsync(id).ConfigureAwait(false);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            // Map users to DTOs
            List<UserDetails> userDetailsList = users
                .Select(user =>
                {
                    DTOs.UserResponses.ReturnCreateUserResponseDTO userDetailsDto = user.ToReturnCreateUserResponseDTO();
                    return new UserDetails(
                        userName: userDetailsDto.UserName,
                        profileUrl: userDetailsDto.ProfileUrl,
                        profilePictureUrl: userDetailsDto.ProfilePictureUrl,
                        new HiddenUserDetails(email: userDetailsDto.Email)
                    );
                })
                .ToList();

            ServiceGetAllSubscribingToUserResponseDto responseDto = new ServiceGetAllSubscribingToUserResponseDto(userDetailsList);
            return Ok(responseDto);
        }
    }
}
