using Renci.SshNet;
using Renci.SshNet.Common;
using reeltok.api.users.utils;
using reeltok.api.users.Interfaces.Services;

namespace reeltok.api.users.Services
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

        public async Task<string> UploadProfilePictureToFileServerAsync(IFormFile imageFile, Guid userId)
        {
            string fileName = ImageUtils.GenerateUniqueFileName(imageFile);
            string usersDirectory = $"{_sftpDirectory}/{userId}";
            string filePath = $"{usersDirectory}/{fileName}";

            using (var sftpClient = new SftpClient(_sftpHostname, _sftpUsername, _sftpPassword))
            {
                try
                {
                    sftpClient.Connect();

                    await EnsureDirectoryExistsAsync(sftpClient, usersDirectory).ConfigureAwait(false);
                    await UploadFileAsync(sftpClient, imageFile, filePath).ConfigureAwait(false);

                    return fileName;
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

        private static async Task EnsureDirectoryExistsAsync(SftpClient sftpClient, string directory)
        {
            if (!await sftpClient.ExistsAsync(directory).ConfigureAwait(false))
            {
                await sftpClient.CreateDirectoryAsync(directory).ConfigureAwait(false);
            }
        }

        private static async Task UploadFileAsync(SftpClient sftpClient, IFormFile imageFile, string filePath)
        {
            using (Stream inStream = imageFile.OpenReadStream())
            {
                await Task.Run(() => sftpClient.UploadFile(inStream, filePath)).ConfigureAwait(false);
            }
        }
    }
}