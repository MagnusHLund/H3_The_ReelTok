using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces
{
    public interface IUsersService
    {
        Task<UserProfileData> CreateUserAsync(UserProfileData user);
        Task<UserProfileData?> GetUserByIdAsync(Guid userId);
        Task UpdateUserAsync(UserProfileData user, Guid userId);

        Task SaveUserImageAsync(Guid userId, IFormFile imageFile, string saveDirectory);
        Task<string> GetUserImageAsync(Guid userId);
        Task UpdateUserImageAsync(Guid userId, IFormFile imageFile, string saveDirectory);
        Task DeleteUserImageAsync(Guid userId, string saveDirectory);

        Task SubscribeAsync(Guid userId, Guid subscribeUserId);
        Task UnsubscribeAsync(Guid userId, Guid subscribeUserId);

        Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId);
        Task RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId);

    }
}