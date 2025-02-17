using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Services
{
    public interface ILikeVideoService
    {
        Task<bool> AddToLikedVideosAsync(LikedVideo likedVideo);
        Task<bool> RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId);

    }
}