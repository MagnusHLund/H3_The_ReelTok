using Moq;
using Xunit;
using reeltok.api.videos.Utils;
using reeltok.api.videos.Services;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Tests.Factories;

namespace reeltok.api.videos.Tests.Services
{
    public class StorageServiceTests
    {

        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AppSettingsUtils _appSettingsUtils;
        private readonly StorageService _storageService;

        public StorageServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(x => x["FileServer:Hostname"])
                .Returns("localhost");
            _mockConfiguration.Setup(x => x["FileServer:Directory"])
                .Returns("videos");
            _mockConfiguration.Setup(x => x["FileServer:Username"])
                .Returns("VideosService");
            _mockConfiguration.Setup(x => x["FileServer:Password"])
                .Returns("SecurePassw0rd");

            _appSettingsUtils = new AppSettingsUtils(_mockConfiguration.Object);
            _storageService = new StorageService(_appSettingsUtils);
        }

        [Fact]
        public async Task UploadVideoToFileServerAsync_UnableToConnect_ThrowIOException()
        {
            // Arrange
            var videoFileMock = new Mock<IFormFile>();
            var fileName = "test_video.mp4";
            var videoContent = Array.Empty<byte>();
            IFormFile thumbnail = TestDataFactory.CreateThumbnailFile();

            using (var memoryStream = new MemoryStream(videoContent))
            {
                videoFileMock.Setup(f => f.FileName).Returns(fileName);
                videoFileMock.Setup(f => f.OpenReadStream()).Returns(memoryStream);

                IFormFile videoFile = videoFileMock.Object;
                VideoEntity video = TestDataFactory.CreateVideoEntity();

                // Act & Assert
                await Assert.ThrowsAsync<IOException>(() => _storageService
                    .UploadVideoFilesUsingSftpAsync(videoFile, thumbnail, video.VideoId, video.UserId));
            }
        }

        [Fact]
        public async Task UploadVideoToFileServerAsync_WithValidParameters_SuccessfullyUploadVideo()
        {
            // Arrange
            var videoFileMock = new Mock<IFormFile>();
            var fileName = "test_video.mp4";
            var videoContent = Array.Empty<byte>();
            IFormFile thumbnail = TestDataFactory.CreateThumbnailFile();

            using (var memoryStream = new MemoryStream(videoContent))
            {
                videoFileMock.Setup(f => f.FileName).Returns(fileName);
                videoFileMock.Setup(f => f.OpenReadStream()).Returns(memoryStream);

                IFormFile videoFile = videoFileMock.Object;
                VideoEntity video = TestDataFactory.CreateVideoEntity();

                // Act
                await _storageService.UploadVideoFilesUsingSftpAsync(videoFile, thumbnail, video.VideoId, video.UserId);

                // Assert
                // No exceptions should be thrown, implying success
            }
        }

        [Fact]
        public async Task DeleteVideoFromFileServerAsync_UnableToFindVideo_ThrowFileNotFoundException()
        {
            // Arrange
            string streamPath = TestDataFactory.CreateVideoEntity().StreamPath;

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(() => _storageService.DeleteVideoFilesUsingSftpAsync(streamPath));
        }

        [Fact]
        public async Task DeleteVideoFromFileServerAsync_WithValidParameters_SuccessfullyUploadVideo()
        {
            // Arrange
            string streamPath = TestDataFactory.CreateVideoEntity().StreamPath;

            // Act
            await _storageService.DeleteVideoFilesUsingSftpAsync(streamPath);

            // Assert
            // No exceptions should be thrown, implying success
        }
    }
}
