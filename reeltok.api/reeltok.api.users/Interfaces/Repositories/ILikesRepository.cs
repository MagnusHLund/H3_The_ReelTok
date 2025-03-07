using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Repositories
{
    public interface ILikesRepository
    {
        Task<bool> AddToLikedVideoAsync(LikedVideoEntity likedVideo);
        Task<bool> RemoveFromLikedVideoAsync(Guid userId, Guid videoId);
    }
}