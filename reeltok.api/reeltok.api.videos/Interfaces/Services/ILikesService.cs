using reeltok.api.videos.Entities;

namespace reeltok.api.videos.Interfaces
{
    public interface ILikesService
    {
        Task<bool> LikeVideoAsync(Guid userId, Guid videoId);
        Task<bool> RemoveLikeFromVideoAsync(Guid userId, Guid videoId);
        Task<List<VideoLikesEntity>> GetLikesForVideosAsync(Guid userId, List<Guid> videoIds);
    }
}
