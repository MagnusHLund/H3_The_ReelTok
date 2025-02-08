using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Services
{
    public class StorageService : IStorageService
    {
        public Task UploadVideoToFileServer(IFormFile video, Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveVideoFromFileServer(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }

        private static string GenerateFilePath(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }
    }
}
