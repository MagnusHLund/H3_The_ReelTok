using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Services
{
    public class SubscriptionsService : BaseService, ISubscriptionsService
    {
        private readonly ISubscriptionsRepository _subscriptionRepository;
        private readonly IUsersService _usersService;

        public SubscriptionsService(ISubscriptionsRepository subscriptionRepository, IUsersService usersService)
        {
            _subscriptionRepository = subscriptionRepository;
            _usersService = usersService;
        }

        public async Task<List<ExternalUserEntity>> GetSubscribersByUserIdAsync(Guid userId)
        {
            List<Guid> SubscribersUserIds = await _subscriptionRepository.GetSubscribersByUserIdAsync(userId)
                .ConfigureAwait(false);

            List<UserEntity> userEntities = await _usersService.GetUsersByIdsAsync(SubscribersUserIds).ConfigureAwait(false);

            List<ExternalUserEntity> subscribers = userEntities.ConvertAll(subscriber => new ExternalUserEntity(subscriber.UserId, subscriber.UserDetails));
            return subscribers;
        }

        public async Task<List<ExternalUserEntity>> GetSubscriptionsByUserIdAsync(Guid userId)
        {
            List<Guid> SubscriptionsUserIds = await _subscriptionRepository.GetSubscriptionsByUserIdAsync(userId)
                .ConfigureAwait(false);

            List<UserEntity> userEntities = await _usersService.GetUsersByIdsAsync(SubscriptionsUserIds).ConfigureAwait(false);

            List<ExternalUserEntity> subscriptions = userEntities.ConvertAll(subscription => new ExternalUserEntity(subscription.UserId, subscription.UserDetails));
            return subscriptions;
        }

        public async Task<bool> SubscribeAsync(SubscriptionDetails subscriptionDetails)
        {
            if (subscriptionDetails.UserId == subscriptionDetails.SubscribingToUserId)
            {
                throw new Exception("User cannot subscribe to themselves!");
            }

            await _usersService.GetUserByIdAsync(subscriptionDetails.UserId).ConfigureAwait(false);
            await _usersService.GetUserByIdAsync(subscriptionDetails.SubscribingToUserId).ConfigureAwait(false);

            SubscriptionEntity subscriptionEntity = new SubscriptionEntity(subscriptionDetails);
            bool IsUserSubscribed = await _subscriptionRepository.AddUserToSubscriptionAsync(subscriptionEntity)
                .ConfigureAwait(false);

            return IsUserSubscribed;
        }

        public async Task<bool> UnsubscribeAsync(SubscriptionDetails subscriptionDetails)
        {
            await _usersService.GetUserByIdAsync(subscriptionDetails.UserId).ConfigureAwait(false);
            await _usersService.GetUserByIdAsync(subscriptionDetails.SubscribingToUserId).ConfigureAwait(false);

            bool IsUserUnsubscribed = await _subscriptionRepository.RemoveUserFromSubscriptionAsync(
                subscriptionDetails.UserId, subscriptionDetails.SubscribingToUserId).ConfigureAwait(false);

            return IsUserUnsubscribed;
        }
    }
}
