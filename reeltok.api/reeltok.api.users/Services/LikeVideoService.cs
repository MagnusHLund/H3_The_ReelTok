using reeltok.api.users.Interfaces.Services;

namespace reeltok.api.users.Services
{
    public class LikeVideoService : ILikeVideoService
    {
        public Task<bool> AddToLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            throw new NotImplementedException();
        }
    }
}