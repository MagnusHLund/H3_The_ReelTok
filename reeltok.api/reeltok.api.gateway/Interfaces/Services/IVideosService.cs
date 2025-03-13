using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities.Videos;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IVideosService
    {
        Task<bool> LikeVideoAsync(Guid videoId);
        Task<bool> RemoveLikeFromVideoAsync(Guid videoId);
        Task<List<VideoForFeedEntity>> GetVideosForFeedAsync(byte amount, Guid userId);
        Task<bool> UploadVideoAsync(VideoUpload video);
        Task<bool> DeleteVideoAsync(Guid videoId);
        Task<List<BaseVideoEntity>> GetVideosForProfileAsync(Guid userId, int pageNumber, byte pageSize);

    }
}
