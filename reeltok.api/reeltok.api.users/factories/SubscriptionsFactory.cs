using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.factories
{
    internal static class SubscriptionsFactory
    {
        internal static SubscriptionEntity CreateNewSubscriptionEntity(SubscriptionDetails subscriptionDetails)
        {
            return new SubscriptionEntity(
                userId: subscriptionDetails.UserId,
                subscribingToUserId: subscriptionDetails.SubscribingToUserId
            );
        }
    }
}