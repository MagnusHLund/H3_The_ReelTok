using reeltok.api.users.Interfaces.Services;

namespace reeltok.api.users.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public Task<bool> SubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnsubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            throw new NotImplementedException();
        }
    }
}