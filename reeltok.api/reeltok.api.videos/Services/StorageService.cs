using Renci.SshNet;
using Renci.SshNet.Common;
using reeltok.api.videos.Utils;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Services
{
    public class StorageService : BaseService, IStorageService
    {
        private const string HostnameConfig = "FileServer:Hostname";
        private const string DirectoryConfig = "FileServer:Directory";
        private const string UsernameConfig = "FileServer:Username";
        private const string PasswordConfig = "FileServer:Password";

        private readonly AppSettingsUtils _appSettingsUtils;
        public StorageService(AppSettingsUtils appSettingsUtils)
        {
            _appSettingsUtils = appSettingsUtils;
        }

        public async Task UploadVideoToFileServerAsync(IFormFile videoFile, Guid videoId, Guid userId)
        {
            string sftpHostname = _appSettingsUtils.GetConfigurationValue(HostnameConfig);
            string sftpDirectory = _appSettingsUtils.GetConfigurationValue(DirectoryConfig);
            string sftpUsername = _appSettingsUtils.GetConfigurationValue(UsernameConfig);
            string sftpPassword = _appSettingsUtils.GetConfigurationValue(PasswordConfig);

            string fileExtension = Path.GetExtension(videoFile.FileName).ToUpperInvariant();
            string userDirectory = $"{sftpDirectory}/{userId}";
            string filePath = $"{userDirectory}/{videoId}{fileExtension}";

            using (var sftpClient = new SftpClient(sftpHostname, sftpUsername, sftpPassword))
            {
                try
                {
                    sftpClient.Connect();

                    if (!sftpClient.Exists(userDirectory))
                    {
                        sftpClient.CreateDirectory(userDirectory);
                    }

                    using (Stream inStream = videoFile.OpenReadStream())
                    {
                        await Task.Run(() => sftpClient.UploadFile(inStream, $"{filePath}")).ConfigureAwait(false);
                    }
                }
                catch (SshException ex)
                {
                    throw new IOException("Unable to connect to the SFTP server!", ex);
                }
                catch (Exception ex)
                {
                    throw new IOException("An error occurred while uploading the video to the SFTP server!", ex);
                }
                finally
                {
                    sftpClient.Disconnect();
                }
            }
        }

        public async Task RemoveVideoFromFileServerAsync(string streamPath)
        {
            string sftpHostname = _appSettingsUtils.GetConfigurationValue(HostnameConfig);
            string sftpDirectory = _appSettingsUtils.GetConfigurationValue(DirectoryConfig);
            string sftpUsername = _appSettingsUtils.GetConfigurationValue(UsernameConfig);
            string sftpPassword = _appSettingsUtils.GetConfigurationValue(PasswordConfig);

            using (var sftpClient = new SftpClient(sftpHostname, sftpUsername, sftpPassword))
            {
                try
                {
                    sftpClient.Connect();
                    string fullPath = $"{sftpDirectory}/{streamPath}";

                    if (!sftpClient.Exists(fullPath))
                    {
                        throw new FileNotFoundException("Video not found.");
                    }

                    await Task.Run(() => sftpClient.DeleteFile(fullPath)).ConfigureAwait(false);

                    sftpClient.Disconnect();
                }
                catch (SshException ex)
                {
                    throw new IOException("Unable to connect to the SFTP server!", ex);
                }
                catch (Exception ex)
                {
                    throw new IOException("An error occurred while removing the video from the SFTP server!", ex);
                }
            }
        }

        public static async Task EnsureValidFileUploadAsync(IFormFile? video)
        {
            if (video == null)
            {
                throw new InvalidOperationException("No video file provided.");
            }

            if (!VideoUtils.IsValidFileExtension(video))
            {
                throw new InvalidOperationException("Invalid video file extension.");
            }

            if (!await VideoUtils.HasVideoStream(video).ConfigureAwait(false))
            {
                throw new InvalidOperationException("The video file does not contain a valid video stream.");
            }

            if (!await VideoUtils.IsVideoMinimumLength(video).ConfigureAwait(false))
            {
                throw new InvalidOperationException("The video file is too short.");
            }
        }
    }
}
