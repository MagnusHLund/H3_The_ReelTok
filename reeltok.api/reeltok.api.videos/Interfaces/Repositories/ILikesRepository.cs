using reeltok.api.videos.Entities;

namespace reeltok.api.videos.Interfaces
{
    public interface ILikesRepository
    {
        Task<List<TotalVideoLikesEntity>> GetTotalLikesForVideosAsync(List<Guid> videoId);
        Task IncrementTotalLikesAsync(Guid videoId);
        Task DecrementTotalLikesAsync(Guid videoId);
    }
}
