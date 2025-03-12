using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IVideosService
    {
        Task<bool> LikeVideoAsync(Guid VideoId);
        Task<bool> RemoveLikeFromVideoAsync(Guid VideoId);
        Task<List<VideoForFeedEntity>> GetVideosForFeedAsync(byte amount);
        Task<bool> UploadVideoAsync(VideoUpload video);
        Task<bool> DeleteVideoAsync(Guid videoId);
        Task<List<BaseVideoEntity>> GetVideosForProfileAsync(Guid userId, uint pageNumber, byte pageSize);

    }
}
