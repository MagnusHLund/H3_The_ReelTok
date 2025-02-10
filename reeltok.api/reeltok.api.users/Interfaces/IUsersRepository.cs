using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> UpdateUserAsync(User user, Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);

        Task<string> GetUserImageAsync(Guid userId);

        Task<List<Guid>> GetAllSubscribersIdAsync(Guid userId); // All who follow User
        Task<List<Guid>> GetAllSubscriptionIdAsync(Guid userId); // All who User follows
        Task AddUserToSubscriptionAsync(Guid userId, Guid subscriptionUserId);
        Task RemoveUserFromSubscriptionAsync(Guid userId, Guid subscriptionUserId);

        Task<List<Guid>> GetAllLikedVideoIdAsync(Guid userId);
        Task AddToLikedVideoAsync(Guid userId, Guid videoId);
        Task RemoveFromLikedVideoAsync(Guid userId, Guid videoId);

    }
}