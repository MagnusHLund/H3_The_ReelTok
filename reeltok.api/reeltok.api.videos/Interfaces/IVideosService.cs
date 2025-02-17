using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Interfaces
{
    public interface IVideosService
    {
        Task<List<VideoEntity>> GetVideosForFeedAsync(Guid userId, uint pageNumber, byte pageSize);
        Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, uint pageNumber, byte pageSize);
        Task<VideoEntity> UploadVideoAsync(VideoUpload video, Guid userId);
        Task DeleteVideoAsync(Guid userId, Guid videosId);
    }
}
