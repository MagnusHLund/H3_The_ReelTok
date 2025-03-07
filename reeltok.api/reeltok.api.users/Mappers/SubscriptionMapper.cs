using reeltok.api.users.ValueObjects;
using reeltok.api.users.DTOs.SubscriptionRequests;

namespace reeltok.api.users.Mappers
{
    internal static class SubscriptionMapper
    {
        internal static SubscriptionDetails ConvertSubscribeRequestDtoToSubscriptionDetails(SubscribeRequestDto requestDto)
        {
            return new SubscriptionDetails(
                requestDto.UserId,
                requestDto.SubscribingToUserId
            );
        }
    }
}