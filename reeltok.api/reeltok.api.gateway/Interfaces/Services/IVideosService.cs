using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IVideosService
    {
        Task<bool> LikeVideoAsync(Guid videoId);
        Task<bool> RemoveLikeFromVideoAsync(Guid videoId);
        Task<List<VideoForFeedEntity>> GetVideosForFeedAsync(byte amount);
        Task<bool> UploadVideoAsync(VideoUpload video);
        Task<bool> DeleteVideoAsync(Guid videoId);
        Task<List<BaseVideoEntity>> GetVideosForProfileAsync(Guid userId, int pageNumber, byte pageSize);

    }
}
