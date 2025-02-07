using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Interfaces
{
    public interface IUsersRepository
    {
        public Task<UserProfileData> CreateUserAsync(UserProfileData user);
        public Task<UserProfileData?> GetUserByIdAsync(Guid id);
        public Task UpdateUserAsync(UserProfileData user, Guid userId);

        public Task<string> GetUserImageAsync(Guid userId);

        public Task<List<Guid>> GetAllSubscribersIdAsync(Guid userId); // All who follow User
        public Task<List<Guid>> GetAllSubscriptionIdAsync(Guid userId); // All who User follows
        public Task AddUserToSubscriptionAsync(Guid userId, Guid subscriptionUserId);
        public Task RemoveUserFromSubscriptionAsync(Guid userId, Guid subscriptionUserId);

        public Task<List<Guid>> GetAllLikedVideoIdAsync(Guid userId);
        public Task AddToLikedVideoAsync(Guid userId, Guid videoId);
        public Task RemoveFromLikedVideoAsync(Guid userId, Guid videoId);

    }
}