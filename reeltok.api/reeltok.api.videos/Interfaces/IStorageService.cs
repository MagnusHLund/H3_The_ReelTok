namespace reeltok.api.videos.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadVideoToFileServerAsync(IFormFile videoFile, Guid videoId, Guid userId);
        Task RemoveVideoFromFileServerAsync(string streamUri);
    }
}
