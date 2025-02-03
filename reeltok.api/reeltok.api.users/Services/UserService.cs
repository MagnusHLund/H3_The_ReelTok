using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces;

namespace reeltok.api.users.Services
{
    public class UserService : IUserService
    {
        public Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(UserProfileData user)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileData?> GetByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            throw new NotImplementedException();
        }

        public Task SubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(UserProfileData user)
        {
            throw new NotImplementedException();
        }
    }
}