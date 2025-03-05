using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Interfaces.Services
{
    public interface ISubscriptionsService
    {
        Task<bool> SubscribeAsync(SubscriptionDetails subscriptionDetails);
        Task<bool> UnsubscribeAsync(SubscriptionDetails subscriptionDetails);
        Task<List<ExternalUserEntity>> GetSubscribersByUserIdAsync(Guid userId);
        Task<List<ExternalUserEntity>> GetSubscriptionsByUserIdAsync(Guid userId);

    }
}