using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces
{
    public interface IUsersService
    {
        public Task CreateAsync(UserProfileData user, Guid userId);
        public Task<UserProfileData?> GetUserByIdAsync(Guid userId);

        public Task SubscribeAsync(Guid userId, Guid subscribeUserId);
        public Task UnsubscribeAsync(Guid userId, Guid subscribeUserId);

        public Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId);
        public Task RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId);

    }
}