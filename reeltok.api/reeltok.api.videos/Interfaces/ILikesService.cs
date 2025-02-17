using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Interfaces
{
    public interface ILikesService
    {
        Task<bool> LikeVideoAsync(Guid userId, Guid videoId);
        Task<bool> RemoveLikeFromVideoAsync(Guid userId, Guid videoId);
        Task<VideoLikes> GetVideoLikesAsync(Guid userId, Guid videoId);
    }
}
