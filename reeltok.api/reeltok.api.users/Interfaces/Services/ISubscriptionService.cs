namespace reeltok.api.users.Interfaces.Services
{
    public interface ISubscriptionService
    {
        Task<bool> SubscribeAsync(Guid userId, Guid subscribeUserId);
        Task<bool> UnsubscribeAsync(Guid userId, Guid subscribeUserId);

    }
}