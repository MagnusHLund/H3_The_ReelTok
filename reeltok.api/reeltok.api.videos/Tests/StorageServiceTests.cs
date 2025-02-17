using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Moq;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Factories;
using reeltok.api.videos.Services;
using reeltok.api.videos.Utils;
using Xunit;

namespace reeltok.api.videos.Tests
{
    public class StorageServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AppSettingsUtils _appSettingsUtils;
        private readonly StorageService _storageService;

        public StorageServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _appSettingsUtils = new AppSettingsUtils(_mockConfiguration.Object);
            _storageService = new StorageService(_appSettingsUtils);
        }

        [Fact]
        public async Task UploadVideoToFileServerAsync_UnableToConnect_ThrowIOException()
        {
            // Arrange
            IFormFile videoFile = new Mock<IFormFile>().Object;
            VideoEntity video = TestDataFactory.CreateVideoEntity();
            _mockConfiguration.Setup(x => x[It.IsAny<string>()])
                .Throws<IOException>();

            // Act & Assert
            await Assert.ThrowsAsync<IOException>(() => _storageService.UploadVideoToFileServerAsync(videoFile, video.VideoId, video.UserId));
        }

        [Fact]
        public async Task UploadVideoToFileServerAsync_WithValidParameters_SuccessfullyUploadVideo()
        {
            // Arrange
            IFormFile videoFile = new Mock<IFormFile>().Object;
            VideoEntity video = TestDataFactory.CreateVideoEntity();

            _mockConfiguration.Setup(x => x[It.IsAny<string>()])
                .Returns("C:/ReelTok/Videos/");

            // Act
            await _storageService.UploadVideoToFileServerAsync(videoFile, video.VideoId, video.UserId);

            // Assert
            // No exceptions should be thrown, implying success
        }

        [Fact]
        public async Task DeleteVideoFromFileServerAsync_UnableToFindVideo_ThrowFileNotFoundException()
        {
            // Arrange
            string streamUrl = TestDataFactory.CreateVideoEntity().StreamUrl;

            _mockConfiguration.Setup(x => x[It.IsAny<string>()])
                .Throws<FileNotFoundException>();

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(() => _storageService.RemoveVideoFromFileServerAsync(streamUrl));
        }

        [Fact]
        public async Task DeleteVideoFromFileServerAsync_WithValidParameters_SuccessfullyUploadVideo()
        {
            // Arrange
            string streamUrl = TestDataFactory.CreateVideoEntity().StreamUrl;

            _mockConfiguration.Setup(x => x["FileServer:Hostname"])
                .Returns("127.0.0.1");

            _mockConfiguration.Setup(x => x["FileServer:Directory"])
                .Returns("C:/ReelTok/Videos/");

            _mockConfiguration.Setup(x => x["FileServer:Username"])
                .Returns("VideoServiceApi");

            _mockConfiguration.Setup(x => x["FileServer:Password"])
                .Returns("VerySecurePassword");

            // Act
            await _storageService.RemoveVideoFromFileServerAsync(streamUrl);

            // Assert
            // No exceptions should be thrown, implying success
        }
    }
}
