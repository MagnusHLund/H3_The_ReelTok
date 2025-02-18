namespace reeltok.api.videos.Interfaces
{
    public interface IStorageService
    {
        Task UploadVideoToFileServerAsync(IFormFile videoFile, Guid videoId, Guid userId);
        Task RemoveVideoFromFileServerAsync(string streamPath);
    }
}
