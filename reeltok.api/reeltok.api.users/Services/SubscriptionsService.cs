using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Services
{
    public class SubscriptionsService : BaseService, ISubscriptionsService
    {
        private readonly ISubscriptionsRepository _subscriptionRepository;

        public SubscriptionsService(ISubscriptionsRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<List<Guid>> GetAllSubscribersIdAsync(Guid userId)
        {
            List<Guid> SubscribersId = await _subscriptionRepository.GetAllSubscribersIdAsync(userId)
                .ConfigureAwait(false);

            return SubscribersId;
        }

        public async Task<List<Guid>> GetAllSubscriptionIdAsync(Guid userId)
        {
            List<Guid> SubscriptionId = await _subscriptionRepository.GetAllSubscriptionIdAsync(userId)
                .ConfigureAwait(false);

            return SubscriptionId;
        }

        public async Task<bool> SubscribeAsync(Subscription subscription)
        {
            bool IsUserSubscribed = await _subscriptionRepository.AddUserToSubscriptionAsync(subscription)
                .ConfigureAwait(false);

            return IsUserSubscribed;
        }

        public async Task<bool> UnsubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            bool IsUserUnsubscribed = await _subscriptionRepository.RemoveUserFromSubscriptionAsync(userId, subscribeUserId)
                .ConfigureAwait(false);

            return IsUserUnsubscribed;
        }
    }
}
