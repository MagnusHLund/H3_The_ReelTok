using reeltok.api.users.DTOs.SubscriptionRequests;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Mappers
{
    public static class SubscriptionMapper
    {
        public static SubscribptionDetails ToSubscriptionFromCreateDTO(this SubscribeRequestDto dto)
        {
            return new SubscribptionDetails(
                dto.SubscriberUserId,
                dto.SubscribingToUserId
            );
        }
    }
}