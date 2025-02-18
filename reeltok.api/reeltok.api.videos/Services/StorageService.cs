using SharpCifs.Smb;
using SharpCifs.Util.Sharpen;
using reeltok.api.videos.Utils;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Services
{
    public class StorageService : BaseService, IStorageService
    {
        // TODO: Implement queue for uploading / deleting videos?
        private const string HostnameConfig = "FileServer:Hostname";
        private const string DirectoryConfig = "FileServer:Directory";
        private const string UsernameConfig = "FileServer:Username";
        private const string PasswordConfig = "FileServer:Password";

        private readonly AppSettingsUtils _appSettingsUtils;
        public StorageService(AppSettingsUtils appSettingsUtils)
        {
            _appSettingsUtils = appSettingsUtils;
        }

        public async Task<string> UploadVideoToFileServerAsync(IFormFile videoFile, Guid videoId, Guid userId)
        {
            string smbHostname = _appSettingsUtils.GetConfigurationValue(HostnameConfig);
            string smbDirectory = _appSettingsUtils.GetConfigurationValue(DirectoryConfig);
            string smbUsername = _appSettingsUtils.GetConfigurationValue(UsernameConfig);
            string smbPassword = _appSettingsUtils.GetConfigurationValue(PasswordConfig);

            string fileExtension = Path.GetExtension(videoFile.FileName).ToUpperInvariant();

            string filePath = GenerateFilePath(userId, videoId, fileExtension);
            string fullPath = $"smb://{smbHostname}/{smbDirectory}/{filePath}";

            try
            {
                NtlmPasswordAuthentication auth = new NtlmPasswordAuthentication(null, smbUsername, smbPassword);
                SmbFile smbFile = new SmbFile(fullPath, auth);

                using (Stream inStream = videoFile.OpenReadStream())
                {
                    using (OutputStream outStream = smbFile.GetOutputStream())
                    {
                        await inStream.CopyToAsync(outStream).ConfigureAwait(false);
                    }
                }
            }
            catch (IOException ex)
            {
                throw new IOException("Unable to connect to the file server!");
            }

            return filePath;
        }

        // TODO: maybe streamUrl isn't the best name here. Should probably be changed elsewhere too.
        public async Task RemoveVideoFromFileServerAsync(string streamUrl)
        {
            string smbHostname = _appSettingsUtils.GetConfigurationValue(HostnameConfig);
            string smbDirectory = _appSettingsUtils.GetConfigurationValue(DirectoryConfig);
            string smbUsername = _appSettingsUtils.GetConfigurationValue(UsernameConfig);
            string smbPassword = _appSettingsUtils.GetConfigurationValue(PasswordConfig);

            string fullPath = $"smb://{smbHostname}/{smbDirectory}/{streamUrl}";

            try
            {
                NtlmPasswordAuthentication auth = new NtlmPasswordAuthentication(null, smbUsername, smbPassword);
                SmbFile smbFile = new SmbFile(fullPath, auth);

                if (!smbFile.Exists())
                {
                    throw new FileNotFoundException("Video not found.");
                }

                await Task.Run(() => smbFile.Delete()).ConfigureAwait(false);
            }
            catch (IOException ex)
            {
                throw new IOException("Unable to connect to the file server!");
            }
        }

        private static string GenerateFilePath(Guid userId, Guid videoId, string fileExtension)
        {
            return Path.Combine(userId.ToString(), videoId.ToString() + fileExtension);
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
