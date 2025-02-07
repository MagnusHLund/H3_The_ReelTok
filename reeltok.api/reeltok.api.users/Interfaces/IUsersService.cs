using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces
{
    public interface IUsersService
    {
        public Task CreateUserAsync(UserProfileData user);
        public Task<UserProfileData?> GetUserByIdAsync(Guid userId);
        public Task UpdateUserAsync(UserProfileData user, Guid userId);

        public Task SaveUserImageAsync(Guid userId, IFormFile imageFile, string saveDirectory);
        public Task<string> GetUserImageAsync(Guid userId);
        public Task UpdateUserImageAsync(Guid userId, IFormFile imageFile, string saveDirectory);
        public Task DeleteUserImageAsync(Guid userId, string saveDirectory);

        public Task SubscribeAsync(Guid userId, Guid subscribeUserId);
        public Task UnsubscribeAsync(Guid userId, Guid subscribeUserId);

        public Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId);
        public Task RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId);

    }
}