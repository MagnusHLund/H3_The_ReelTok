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

        public async Task UploadVideoFilesUsingSftpAsync(IFormFile videoFile, IFormFile thumbnailFile, Guid videoId, Guid userId)
        {
            string videoFileExtension = Path.GetExtension(videoFile.FileName).ToUpperInvariant();
            string userDirectory = $"{_sftpDirectory}/{userId}";
            string filePath = $"{userDirectory}/{videoId}";

            string videoFilePath = $"{filePath}{videoFileExtension}";
            string thumbnailFilePath = $"{filePath}.jpg";

            using (var sftpClient = new SftpClient(_sftpHostname, _sftpUsername, _sftpPassword))
            {
                try
                {
                    sftpClient.Connect();

                    if (!await sftpClient.ExistsAsync(userDirectory).ConfigureAwait(false))
                    {
                        await sftpClient.CreateDirectoryAsync(userDirectory).ConfigureAwait(false);
                    }

                    await UploadFileAsync(sftpClient, videoFile, videoFilePath).ConfigureAwait(false);
                    await UploadFileAsync(sftpClient, thumbnailFile, thumbnailFilePath).ConfigureAwait(false);
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

        public async Task DeleteVideoFilesUsingSftpAsync(string streamPath)
        {
            string videoFilePath = $"{_sftpDirectory}/{streamPath}";
            string thumbnailFilePath = $"{FileNameWithoutExtension(videoFilePath)}.jpg";

            using (var sftpClient = new SftpClient(_sftpHostname, _sftpUsername, _sftpPassword))
            {
                try
                {
                    sftpClient.Connect();

                    await DeleteFileAsync(sftpClient, videoFilePath).ConfigureAwait(false);
                    await DeleteFileAsync(sftpClient, thumbnailFilePath).ConfigureAwait(false);
                }
                catch (SshException ex)
                {
                    throw new IOException("Unable to connect to the SFTP server!", ex);
                }
                catch (Exception ex)
                {
                    throw new IOException("An error occurred while removing the video from the SFTP server!", ex);
                }
                finally
                {
                    sftpClient.Disconnect();
                }
            }
        }

        private static async Task UploadFileAsync(SftpClient sftpClient, IFormFile file, string filePath)
        {
            using (Stream inStream = file.OpenReadStream())
            {
                await Task.Run(() => sftpClient.UploadFile(inStream, $"{filePath}")).ConfigureAwait(false);
            }
        }

        private static async Task DeleteFileAsync(SftpClient sftpClient, string filePath)
        {
            if (await sftpClient.ExistsAsync(filePath).ConfigureAwait(false))
            {
                await Task.Run(() => sftpClient.DeleteFile(filePath)).ConfigureAwait(false);
            }
        }

        private static string FileNameWithoutExtension(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }
    }
}
