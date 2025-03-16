namespace reeltok.api.videos.Interfaces
{
    public interface IStorageService
    {
        Task UploadVideoFilesUsingSftpAsync(IFormFile videoFile, IFormFile thumbnailFile, Guid videoId, Guid userId);
        Task DeleteVideoFilesUsingSftpAsync(string streamPath);
    }
}
