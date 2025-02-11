using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Repositories;
using reeltok.api.users.Interfaces.Services;

namespace reeltok.api.users.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public Task<List<Guid>> GetAllSubscribersIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guid>> GetAllSubscriptionIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SubscribeAsync(Subscription subscription)
        {
            bool IsUserSubscribed;

            try
            {
                IsUserSubscribed = await _subscriptionRepository.AddUserToSubscriptionAsync(subscription).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Handle the error, you can log it or throw a custom exception if needed
                throw new InvalidOperationException("Subscription failed.", ex);
            }

            return IsUserSubscribed;
        }

        public async Task<bool> UnsubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            bool IsUserUnsubscribed;

            try
            {
                IsUserUnsubscribed = await _subscriptionRepository.RemoveUserFromSubscriptionAsync(userId, subscribeUserId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Handle the error, you can log it or throw a custom exception if needed
                throw new InvalidOperationException("Unsubscription failed.", ex);
            }

            return IsUserUnsubscribed;
        }
    }
}