using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Services
{
    public interface ISubscriptionService
    {
        Task<bool> SubscribeAsync(Subscription subscription);
        Task<bool> UnsubscribeAsync(Guid userId, Guid subscribeUserId);
        Task<List<Guid>> GetAllSubscribersIdAsync(Guid userId);
        Task<List<Guid>> GetAllSubscriptionIdAsync(Guid userId);

    }
}