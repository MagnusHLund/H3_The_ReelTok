using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Repositories
{
    public class LikeVideoRepository : ILikeVideoRepository
    {
        public Task<bool> AddToLikedVideoAsync(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromLikedVideoAsync(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }
    }
}