using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Repositories
{
    public interface ISubscriptionsRepository
    {
        Task<List<Guid>> GetSubscribersByUserIdAsync(Guid userId, int pageNumber, byte pageSize); // All who follow User
        Task<List<Guid>> GetSubscriptionsByUserIdAsync(Guid userId, int pageNumber, byte pageSize); // All who User follows
        Task<bool> AddUserToSubscriptionAsync(SubscriptionEntity subscription);
        Task<bool> RemoveUserFromSubscriptionAsync(Guid userId, Guid subscriptionUserId);

    }
}
