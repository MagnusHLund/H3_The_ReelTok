using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Interfaces.Services
{
    public interface ILikesService
    {
        Task<bool> AddToLikedVideosAsync(LikedDetails likedDetails);
        Task<bool> RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId);

    }
}