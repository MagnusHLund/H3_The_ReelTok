using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.DTOs.SubscriptionRequests;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Mappers;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Controllers
{
    [Route("api/subscriptions")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService, IUsersService usersService)
        {
            _usersService = usersService;
            _subscriptionService = subscriptionService;
        }

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

            User? existingUser = await _usersService.GetUserByIdAsync(subscription.SubscriberUserId).ConfigureAwait(false);

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
    }
}