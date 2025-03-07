using Renci.SshNet;
using Renci.SshNet.Common;
using reeltok.api.videos.Utils;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Services
{
    public class StorageService : IStorageService
    {
        private readonly string _sftpHostname;
        private readonly string _sftpDirectory;
        private readonly string _sftpUsername;
        private readonly string _sftpPassword;

        public StorageService(AppSettingsUtils appSettingsUtils)
        {
            string baseFileServerAppSettingsConfig = "FileServer";

            _sftpHostname = appSettingsUtils.GetConfigurationValue($"{baseFileServerAppSettingsConfig}:Hostname");
            _sftpDirectory = appSettingsUtils.GetConfigurationValue($"{baseFileServerAppSettingsConfig}:Directory");
            _sftpUsername = appSettingsUtils.GetConfigurationValue($"{baseFileServerAppSettingsConfig}:Username");
            _sftpPassword = appSettingsUtils.GetConfigurationValue($"{baseFileServerAppSettingsConfig}:Password");
        }

        public async Task UploadVideoToFileServerAsync(IFormFile videoFile, Guid videoId, Guid userId)
        {
            string fileExtension = Path.GetExtension(videoFile.FileName).ToUpperInvariant();
            string userDirectory = $"{_sftpDirectory}/{userId}";
            string filePath = $"{userDirectory}/{videoId}{fileExtension}";

            using (var sftpClient = new SftpClient(_sftpHostname, _sftpUsername, _sftpPassword))
            {
                try
                {
                    sftpClient.Connect();

                    if (!await sftpClient.ExistsAsync(userDirectory).ConfigureAwait(false))
                    {
                        await sftpClient.CreateDirectoryAsync(userDirectory).ConfigureAwait(false);
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
            using (var sftpClient = new SftpClient(_sftpHostname, _sftpUsername, _sftpPassword))
            {
                try
                {
                    sftpClient.Connect();
                    string fullPath = $"{_sftpDirectory}/{streamPath}";

                    if (!await sftpClient.ExistsAsync(fullPath).ConfigureAwait(false))
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
    }
}
