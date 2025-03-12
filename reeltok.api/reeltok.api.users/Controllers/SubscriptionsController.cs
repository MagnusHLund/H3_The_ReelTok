using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.Mappers;
using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.ActionFilters;
using reeltok.api.users.DTOs.Subscribe;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.DTOs.GetSubscribers;
using reeltok.api.users.DTOs.GetSubscriptions;
using reeltok.api.users.DTOs.SubscriptionRequests;

namespace reeltok.api.users.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionsService _subscriptionService;

        public SubscriptionsController(ISubscriptionsService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeToUserAsync([FromBody] SubscribeRequestDto request)
        {
            SubscriptionDetails subscriptionDetails = SubscriptionMapper.ConvertSubscribeRequestDtoToSubscriptionDetails(request);

            bool success = await _subscriptionService.SubscribeAsync(subscriptionDetails).ConfigureAwait(false);

            SubscribeResponseDto response = new SubscribeResponseDto(success);
            return Ok(response);
        }

        [HttpDelete("subscribe")]
        public async Task<IActionResult> UnsubscribeToUserAsync([FromQuery] Guid userId, [FromQuery] Guid unsubscribingToUserId)
        {
            SubscriptionDetails subscriptionDetails = new SubscriptionDetails(userId, unsubscribingToUserId);

            bool success = await _subscriptionService.UnsubscribeAsync(subscriptionDetails).ConfigureAwait(false);

            UnsubscribeResponseDto response = new UnsubscribeResponseDto(success);
            return Ok(response);
        }

        [HttpGet("subscribers")]
        public async Task<IActionResult> GetUserSubscribersAsync(
            [FromQuery] Guid userId,
            [FromQuery] int pageNumber,
            [FromQuery] byte pageSize
        )
        {
            List<ExternalUserEntity> subscribers = await _subscriptionService
                .GetSubscribersByUserIdAsync(userId, pageNumber, pageSize)
                .ConfigureAwait(false);

            GetSubscribersResponseDto response = new GetSubscribersResponseDto(subscribers);
            return Ok(response);
        }

        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetUserSubscriptionsAsync(
            [FromQuery] Guid userId,
            [FromQuery] int pageNumber,
            [FromQuery] byte pageSize
        )
        {
            List<ExternalUserEntity> subscriptions = await _subscriptionService
                .GetSubscriptionsByUserIdAsync(userId, pageNumber, pageSize)
                .ConfigureAwait(false);

            GetSubscriptionsResponseDto response = new GetSubscriptionsResponseDto(subscriptions);
            return Ok(response);
        }
    }
}
