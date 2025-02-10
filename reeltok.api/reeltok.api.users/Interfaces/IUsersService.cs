using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces
{
    public interface IUsersService
    {
        Task<Users> CreateUserAsync(Users user);
        Task<Users?> GetUserByIdAsync(Guid userId);
        Task<Users?> UpdateUserAsync(Users user, Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);

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