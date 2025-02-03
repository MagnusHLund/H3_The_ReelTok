using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(UserDetails user);
        Task<UserProfileData?> GetUserByIdAsync(Guid id);

        Task AddUserToSubscriptionAsync(Guid userId, Guid subscriptionUserId);

        Task AddToLikedVideoAsync(Guid userId, Guid videoId);
        Task RemoveFromLikedVideoAsync(Guid userId, Guid videoId);

    }
}