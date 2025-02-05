using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Interfaces
{
    public interface IUsersRepository
    {
        public Task CreateUserAsync(UserDetails user);
        public Task<UserProfileData?> GetUserByIdAsync(Guid id);

        public Task AddUserToSubscriptionAsync(Guid userId, Guid subscriptionUserId);
        public Task RemoveUserFromSubscriptionAsync(Guid userId, Guid subscriptionUserId);

        public Task AddToLikedVideoAsync(Guid userId, Guid videoId);
        public Task RemoveFromLikedVideoAsync(Guid userId, Guid videoId);

    }
}