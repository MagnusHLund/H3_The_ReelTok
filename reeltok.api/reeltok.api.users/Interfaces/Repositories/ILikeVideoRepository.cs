using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Repositories
{
    public interface ILikeVideoRepository
    {
        Task<bool> AddToLikedVideoAsync(LikedVideo likedVideo);
        Task<bool> RemoveFromLikedVideoAsync(Guid userId, Guid videoId);
    }
}