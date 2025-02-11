using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public Task<bool> AddUserToSubscriptionAsync(Guid userId, Guid subscriptionUserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guid>> GetAllSubscribersIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guid>> GetAllSubscriptionIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromSubscriptionAsync(Guid userId, Guid subscriptionUserId)
        {
            throw new NotImplementedException();
        }
    }
}