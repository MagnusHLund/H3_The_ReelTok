using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Interfaces
{
    public interface IVideosService
    {
        Task<List<VideoForFeedEntity>> GetVideosForFeedAsync(Guid userId, byte amount);
        Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, uint pageNumber, byte pageSize);
        Task<VideoEntity> UploadVideoAsync(VideoUpload video, Guid userId, byte category);
        Task DeleteVideoAsync(Guid userId, Guid videoId);
    }
}
