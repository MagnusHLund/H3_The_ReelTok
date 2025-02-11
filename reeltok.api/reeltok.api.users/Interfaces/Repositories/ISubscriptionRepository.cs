using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<List<Guid>> GetAllSubscribersIdAsync(Guid userId); // All who follow User
        Task<List<Guid>> GetAllSubscriptionIdAsync(Guid userId); // All who User follows
        Task<bool> AddUserToSubscriptionAsync(Subscription subscription);
        Task<bool> RemoveUserFromSubscriptionAsync(Guid userId, Guid subscriptionUserId);

    }
}