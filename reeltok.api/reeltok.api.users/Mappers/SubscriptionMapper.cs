using reeltok.api.users.DTOs.SubscriptionRequests;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Mappers
{
    public static class SubscriptionMapper
    {
        public static SubscriptionDetails ToSubscriptionFromCreateDTO(SubscribeRequestDto requestDto)
        {
            return new SubscriptionDetails(
                requestDto.UserId,
                requestDto.SubscribingToUserId
            );
        }
    }
}