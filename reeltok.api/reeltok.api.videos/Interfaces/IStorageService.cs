namespace reeltok.api.videos.Interfaces
{
    public interface IStorageService
    {
        Task UploadVideoToFileServerAsync(IFormFile video, Guid userId, Guid videoId);
        Task RemoveVideoFromFileServerAsync(Guid userId, Guid videoId);
    }
}
