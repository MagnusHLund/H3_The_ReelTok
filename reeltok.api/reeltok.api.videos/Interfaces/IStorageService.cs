namespace reeltok.api.videos.Interfaces
{
    public interface IStorageService
    {
        Task<Uri> UploadVideoToFileServerAsync(IFormFile videoFile, Guid videoId, Guid userId);
        Task RemoveVideoFromFileServerAsync(string streamUri);
    }
}
