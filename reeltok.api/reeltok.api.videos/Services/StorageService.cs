using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Utils;

namespace reeltok.api.videos.Services
{
    public class StorageService : BaseService, IStorageService
    {
        private const string DirectoryConfig = "FileServer:Directory";
        private const string UsernameConfig = "FileServer:Username";
        private const string PasswordConfig = "FileServer:Password";

        private readonly AppSettingsUtils _appSettingsUtils;
        public StorageService(AppSettingsUtils appSettingsUtils)
        {
            _appSettingsUtils = appSettingsUtils;
        }

        public async Task UploadVideoToFileServerAsync(VideoEntity video)
        {


            throw new NotImplementedException();
        }

        public Task RemoveVideoFromFileServerAsync(Uri streamUrl)
        {
            throw new NotImplementedException();
        }

        private static string GenerateFilePath(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }

        public static async Task EnsureValidFileUploadAsync(IFormFile? video)
        {
            // TODO: Better exceptions
            if (video == null)
            {
                throw new InvalidOperationException("");
            }

            if (VideoUtils.IsValidFileExtension(video))
            {
                throw new InvalidOperationException("");
            }

            if (!await VideoUtils.HasVideoStream(video).ConfigureAwait(false))
            {
                throw new InvalidOperationException("");
            }

            if (!await VideoUtils.IsVideoMinimumLength(video).ConfigureAwait(false))
            {
                throw new InvalidOperationException("");
            }
        }
    }
}
