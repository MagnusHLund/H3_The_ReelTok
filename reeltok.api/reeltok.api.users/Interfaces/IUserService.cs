using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserProfileData user);
        Task<UserProfileData?> GetByIdAsync(Guid userId);

        Task SubscribeAsync(Guid userId, Guid subscribeUserId);
        Task UnsubscribeAsync(Guid userId, Guid subscribeUserId);

        Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId);
        Task RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId);

    }
}